// --- 1. KHAI BÁO DỮ LIỆU & HELPERS ---
const STATUS = {
    NEW: 'new', COOKING: 'cooking', COOKED: 'cooked', SHIPPING: 'shipping', DONE: 'done', CANCEL: 'cancel'
};

let orders = [];
let tableBookings = [];
let tables = [];
let currentEditingTableId = null;

function getToken() {
    return document.querySelector('meta[name="csrf-token"]')?.content ?? '';
}

async function safeJson(res) {
    const txt = await res.text();
    try { return txt ? JSON.parse(txt) : null; } catch { return null; }
}

async function get(url) {
    const res = await fetch(url, { credentials: 'same-origin', headers: { 'Accept': 'application/json' } });
    if (!res.ok) {
        const payload = await safeJson(res);
        throw new Error(payload?.message ?? res.statusText ?? `HTTP ${res.status}`);
    }
    return await res.json();
}

async function postForm(url, params = {}) {
    const body = new URLSearchParams(params).toString();
    const res = await fetch(url, {
        method: 'POST',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Accept': 'application/json',
            'RequestVerificationToken': getToken()
        },
        body
    });
    if (!res.ok) {
        const payload = await safeJson(res);
        throw new Error(payload?.message ?? res.statusText ?? `HTTP ${res.status}`);
    }
    return await res.json();
}

// --- 2. XỬ LÝ CHUYỂN TAB ---
document.querySelectorAll('.sidebar-group a').forEach(link => {
    link.addEventListener('click', function (e) {
        e.preventDefault();
        const targetTab = this.getAttribute('data-tab');

        document.querySelectorAll('.sidebar-group a').forEach(a => a.classList.remove('active'));
        this.classList.add('active');

        document.querySelectorAll('.admin-tab-content').forEach(section => section.classList.remove('active'));
        document.getElementById(targetTab).classList.add('active');

        if (targetTab === 'tab-so-do-ban') renderTables();
        if (targetTab === 'tab-dat-ban') renderTableBookings();
        if (targetTab === 'tab-don-hang') renderOrders();
        if (targetTab === 'tab-lich-su') initHistory();
    });
});

// --- 3. BACKEND LOADERs ---
async function loadTables() {
    try {
        const data = await get('/Admin/GetTables');
        // normalize to expected shape
        tables = (data || []).map(t => ({
            id: t.id,
            status: t.status,
            guest: t.note || t.tableCode || (t.guest || null)
        }));
        renderTables();
        updateRightStats();
    } catch (err) {
        console.error('loadTables error', err);
    }
}

async function loadBookings() {
    try {
        const data = await get('/Admin/GetBookings');
        tableBookings = data || [];
        renderTableBookings();
    } catch (err) {
        console.error('loadBookings error', err);
    }
}

async function loadOrders() {
    try {
        const data = await get('/Admin/GetOrders');
        orders = data || [];
        renderOrders();
        updateRightStats();
    } catch (err) {
        console.error('loadOrders error', err);
    }
}

// --- 4. QUẢN LÝ ĐƠN ĐẶT BÀN ---
function renderTableBookings() {
    const body = document.getElementById('tableBookingBody');
    if (!body) return;
    body.innerHTML = tableBookings.map(bk => `
        <tr>
            <td>#${bk.id}</td>
            <td>${bk.customerName ?? bk.customer}</td>
            <td>${bk.customerPhone ?? bk.phone ?? '-'}</td>
            <td>${bk.bookingDate ?? bk.time ?? '-'}</td>
            <td>
                ${bk.status === 'pending' || bk.status === 'new' ? `
                    <button class="btn-action btn-confirm" onclick="confirmBooking('${bk.id}')">Chấp nhận & Xếp bàn</button>
                    <button class="btn-action btn-cancel" onclick="cancelBooking('${bk.id}')">Hủy đơn</button>
                ` : `<span class="status-badge status-done">Đã xếp chỗ</span>`}
            </td>
        </tr>
    `).join('');
}

async function confirmBooking(id) {
    try {
        // try to confirm booking on server; tableId optional
        await postForm('/Admin/ConfirmBooking', { id });
        // refresh
        await loadBookings();
        await loadTables();
        alert('Đã xác nhận đặt bàn. Danh sách đã được cập nhật.');
        // optional: switch to table tab
        document.querySelector('[data-tab="tab-so-do-ban"]').click();
    } catch (err) {
        console.error('confirmBooking error', err);
        alert('Lỗi khi xác nhận đặt bàn: ' + err.message);
    }
}

async function cancelBooking(id) {
    if (!confirm('Từ chối đơn đặt bàn này?')) return;
    try {
        await postForm('/Admin/CancelBooking', { id });
        await loadBookings();
        updateRightStats();
    } catch (err) {
        console.error('cancelBooking error', err);
        alert('Lỗi khi hủy đặt bàn: ' + err.message);
    }
}

// --- 5. SƠ ĐỒ BÀN & MODAL FORM ---
function renderTables() {
    const container = document.getElementById('tableGridContainer');
    if (!container) return;
    container.innerHTML = tables.map(t => `
        <div class="table-item ${t.status}" onclick="handleTableAction(${t.id})">
            <i class="fas fa-chair"></i>
            <span>Bàn số ${t.id}</span>
            <small>${t.guest || 'Trống'}</small>
        </div>
    `).join('');
    updateRightStats();
}

async function handleTableAction(id) {
    const table = tables.find(t => t.id === id);
    currentEditingTableId = id;

    if (!table) return;

    if (table.status === 'available') {
        document.getElementById('tableModal').style.display = 'flex';
        document.getElementById('modalTitle').innerText = `Đặt trước bàn số ${id}`;
    } else if (table.status === 'reserved') {
        const choice = confirm(`Khách [${table.guest}] đã đến nhận bàn số ${id}?\nOK = BẮT ĐẦU SỬ DỤNG | Cancel = HỦY ĐẶT TRƯỚC`);
        if (choice) {
            try {
                await postForm('/Admin/UpdateTableStatus', { id, status: 'occupied', note: '' });
                await loadTables();
                alert('Đã chuyển trạng thái sang: Đang sử dụng.');
            } catch (err) { console.error(err); alert('Lỗi: ' + err.message); }
        } else {
            if (confirm("Xác nhận HỦY đặt chỗ của bàn này?")) {
                try {
                    await postForm('/Admin/UpdateTableStatus', { id, status: 'available', note: '' });
                    await loadTables();
                } catch (err) { console.error(err); alert('Lỗi: ' + err.message); }
            }
        }
    } else if (table.status === 'occupied') {
        if (confirm(`Khách ${table.guest} tại bàn ${id} đã dùng xong và thanh toán?`)) {
            try {
                await postForm('/Admin/UpdateTableStatus', { id, status: 'available', note: '' });
                await loadTables();
            } catch (err) { console.error(err); alert('Lỗi: ' + err.message); }
        }
    }
}

// Hàm lưu từ Form (Mặc định là Reserved)
async function saveTableInfo() {
    const name = document.getElementById('guestName').value.trim();
    const phone = document.getElementById('guestPhone').value.trim();
    const count = document.getElementById('guestCount').value;

    if (!name || !phone || !count) {
        alert("Vui lòng nhập đầy đủ thông tin khách hàng!");
        return;
    }

    const note = `${name} (${count} người) - ${phone}`;

    try {
        await postForm('/Admin/UpdateTableStatus', {
            id: currentEditingTableId,
            status: 'reserved',
            note
        });
        alert(`Đã đặt trước bàn ${currentEditingTableId} cho ${name}.`);
        closeModal();
        await loadTables();
    } catch (err) {
        console.error('saveTableInfo error', err);
        alert('Lỗi khi lưu đặt bàn: ' + err.message);
    }
}

function closeModal() {
    const m = document.getElementById('tableModal');
    if (m) m.style.display = 'none';
    const a = document.getElementById('guestName'); if (a) a.value = '';
    const b = document.getElementById('guestPhone'); if (b) b.value = '';
    const c = document.getElementById('guestCount'); if (c) c.value = '';
}

// --- 6. QUẢN LÝ ĐƠN HÀNG (RÀNG BUỘC BẾP) ---
function renderOrders() {
    const body = document.getElementById('orderTableBody');
    if (!body) return;
    const activeOrders = orders.filter(o => o.status !== 'done' && o.status !== 'cancelled' && o.status !== 'cancel');
    body.innerHTML = activeOrders.map(o => `
        <tr>
            <td>#${o.id}</td>
            <td>${o.customerName ?? o.customer}</td>
            <td>${(o.items ? o.items.map(i => i.menuItemName).join(', ') : o.detail) || '-'}</td>
            <td><span class="status-badge status-${o.status}">${getStatusText(o.status)}</span></td>
            <td>${renderActionButtons(o)}</td>
        </tr>
    `).join('');
}

function renderActionButtons(order) {
    if (order.status === STATUS.NEW) return `<button class="btn-action btn-confirm" onclick="updateStatus('${order.id}', '${STATUS.COOKING}')">Xác nhận</button>`;
    if (order.status === STATUS.COOKING) return `<button class="btn-action btn-confirm" onclick="updateStatus('${order.id}', '${STATUS.COOKED}')">Bếp báo xong</button>`;
    if (order.status === STATUS.COOKED) return `<button class="btn-action btn-confirm" style="background:#17a2b8" onclick="updateStatus('${order.id}', '${STATUS.SHIPPING}')">Giao hàng</button>`;
    if (order.status === STATUS.SHIPPING) return `<button class="btn-action btn-confirm" onclick="updateStatus('${order.id}', '${STATUS.DONE}')">Hoàn tất</button>`;
    return '';
}

async function updateStatus(id, newStatus) {
    try {
        // admin endpoint expects action name mapping; try to send action param as in backend (confirm/cancel/cooking/etc.)
        // here we send action = newStatus for simplicity; backend may map it
        const res = await postForm('/Admin/UpdateOrderStatus', { id, action: newStatus });
        if (res?.success) {
            await loadOrders();
            updateRightStats();
        } else {
            throw new Error(res?.message ?? 'Unknown error');
        }
    } catch (err) {
        console.error('updateStatus error', err);
        alert('Lỗi khi cập nhật trạng thái đơn: ' + err.message);
    }
}

function getStatusText(s) {
    const m = { 'new': 'Mới', 'cooking': 'Đang nấu', 'cooked': 'Chờ giao', 'shipping': 'Đang giao', 'done': 'Hoàn tất', 'cancel': 'Đã hủy' };
    return m[s] || s;
}

// --- 7. THỐNG KÊ CHI TIẾT (CỘT PHẢI) ---
function updateRightStats() {
    const today = new Date().toISOString().split('T')[0];
    const todayOrders = orders.filter(o => (o.createdAt?.split?.('T')?.[0] ?? o.date) === today);

    document.getElementById('stat-new').innerText = todayOrders.filter(o => o.status === STATUS.NEW).length;
    document.getElementById('stat-done').innerText = todayOrders.filter(o => o.status === STATUS.DONE).length;
    document.getElementById('stat-cancel').innerText = todayOrders.filter(o => o.status === STATUS.CANCEL || o.status === 'cancelled').length;

    const revenue = todayOrders.filter(o => o.status === STATUS.DONE).reduce((s, o) => s + (o.totalPrice ?? o.price ?? 0), 0);
    const revEl = document.getElementById('stat-revenue');
    if (revEl) revEl.innerText = revenue.toLocaleString() + 'đ';

    const occupied = tables.filter(t => t.status !== 'available').length;
    const percent = tables.length ? Math.round((occupied / tables.length) * 100) : 0;
    const prog = document.getElementById('tableProgress');
    if (prog) prog.value = percent;
    const txt = document.getElementById('tableStatusText');
    if (txt) txt.innerText = `Sử dụng: ${occupied}/${tables.length} bàn (${percent}%)`;
}

// --- 8. LỊCH SỬ / BÁO CÁO ---
function initHistory() {
    const dateInput = document.getElementById('historyDateFilter');
    const today = new Date().toISOString().split('T')[0];
    if (dateInput) {
        dateInput.value = today;
        dateInput.addEventListener('change', (e) => renderHistory(e.target.value));
        renderHistory(today);
    }
}

function renderHistory(selectedDate) {
    const container = document.getElementById('historyContent');
    if (!container) return;

    const dayOrders = orders.filter(o => (o.createdAt?.split?.('T')?.[0] ?? o.date) === selectedDate);

    const stats = {
        total: dayOrders.length,
        done: dayOrders.filter(o => o.status === STATUS.DONE).length,
        cancel: dayOrders.filter(o => o.status === STATUS.CANCEL || o.status === 'cancelled').length,
        pending: dayOrders.filter(o => o.status !== STATUS.DONE && o.status !== STATUS.CANCEL && o.status !== 'cancelled').length,
        revenue: dayOrders.filter(o => o.status === STATUS.DONE).reduce((sum, o) => sum + (o.totalPrice ?? o.price ?? 0), 0)
    };

    container.innerHTML = `
        <div class="history-stats-grid">
            <div class="stats-card-mini">Tổng đơn: <strong>${stats.total}</strong></div>
            <div class="stats-card-mini" style="border-left-color: var(--success-color)">Thành công: <strong>${stats.done}</strong></div>
            <div class="stats-card-mini" style="border-left-color: var(--warning-color)">Đang xử lý: <strong>${stats.pending}</strong></div>
            <div class="stats-card-mini" style="border-left-color: var(--danger-color)">Đã hủy: <strong>${stats.cancel}</strong></div>
        </div>
        <div class="revenue-highlight" style="margin-bottom: 20px; font-size: 1.2em;">
            Tổng doanh thu ngày: <strong style="color: var(--primary-color)">${stats.revenue.toLocaleString()}đ</strong>
        </div>
        <table class="admin-table">
            <thead>
                <tr>
                    <th>Giờ</th>
                    <th>Mã đơn</th>
                    <th>Khách hàng</th>
                    <th>Thành tiền</th>
                    <th>Trạng thái</th>
                </tr>
            </thead>
            <tbody>
                ${dayOrders.length > 0 ? dayOrders.map(o => `
                    <tr>
                        <td>${(o.createdAt ? new Date(o.createdAt).toLocaleTimeString('vi') : (o.time || '--:--'))}</td>
                        <td>#${o.id}</td>
                        <td>${o.customerName ?? o.customer}</td>
                        <td>${((o.totalPrice ?? o.price) || 0).toLocaleString()}đ</td>
                        <td><span class="status-badge status-${o.status}">${getStatusText(o.status)}</span></td>
                    </tr>
                `).join('') : '<tr><td colspan="5" style="text-align:center">Không có giao dịch nào trong ngày này</td></tr>'}
            </tbody>
        </table>
    `;
}

// --- 9. INIT ON LOAD ---
window.addEventListener('DOMContentLoaded', async () => {
    try {
        await Promise.all([loadTables(), loadBookings(), loadOrders()]);
        initHistory();
        updateRightStats();
    } catch (err) {
        console.error('init error', err);
    }
});