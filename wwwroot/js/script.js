// --- 1. Xử lý Dropdown ---
function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

window.onclick = function (event) {
    if (!event.target.matches('.dropbtn') && !event.target.matches('.fa-bars')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

// --- LOGIC GIỎ HÀNG (Dùng cho đặt món) ---
let cart = [];
let foodData = []; // moved up so addToCartById can use it

function addToCart(food) {
    if (cart.length >= 3) {
        alert("Bạn chỉ được chọn tối đa 3 món!");
        return;
    }

    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
    // defensive copy: ensure id exists and is a number
    const id = Number(food.id ?? food.menuItemId ?? food.MenuItemId);
    if (!id || isNaN(id)) {
        console.error('addToCart: invalid item id', food);
        alert('Không thể thêm món (id không hợp lệ). Vui lòng thử lại.');
        return;
    }

    cart.push({
        id,
        name: food.name ?? food.menuItemName ?? '',
        price: food.price ?? 0
    });
    alert(`Đã thêm "${food.name ?? food.menuItemName}" vào giỏ hàng!`);
}

function removeFromCart(index) {
    cart.splice(index, 1);
    openModal('đặt món'); // Render lại modal để cập nhật danh sách
}

async function confirmOrder() {
    if (cart.length === 0) {
        alert("Giỏ hàng của bạn đang trống!");
        return;
    }

    const name = document.getElementById('orderName').value;
    const phone = document.getElementById('orderPhone').value;
    const address = document.getElementById('orderAddress').value;
    const time = document.getElementById('orderTime').value;
    const note = document.getElementById('orderNote').value;

    if (!name || !phone || !time || !address) {
        alert("Vui lòng nhập đầy đủ thông tin (tên, số điện thoại, địa chỉ, thời gian)!");
        return;
    }

    const orderData = {
        customerName: name,
        customerPhone: phone,
        deliveryAddress: address,
        pickupTime: new Date(time).toISOString(),
        note: note,
        items: cart.map(item => ({
            menuItemId: item.id,
            quantity: 1
        }))
    };

    try {
        const res = await fetch('/Home/CreateOrder', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(orderData)
        });

        if (!res.ok) {
            let text;
            try { text = (await res.json()).message ?? res.statusText; }
            catch { text = res.statusText || `HTTP ${res.status}`; }
            alert(`Lỗi server: ${text}`);
            return;
        }

        const data = await res.json();

        if (!data.success) {
            alert(data.message);
            return;
        }

        alert(`✅ Đặt hàng thành công!\nMã đơn: ${data.orderCode}`);
        cart = [];
        closeModal();

    } catch (err) {
        console.error(err);
        alert("Lỗi khi gửi đơn! Vui lòng kiểm tra kết nối và thử lại.");
    }
}

// Tra cuu don hang va dat ban
async function findOrder() {
    const phoneInput = document.getElementById('searchPhone').value;
    const resultContainer = document.getElementById('searchResult');

    if (!phoneInput) {
        alert("Vui lòng nhập số điện thoại!");
        return;
    }

    resultContainer.innerHTML = "Đang tìm...";

    try {
        const res = await fetch(`/Home/Search?phone=${encodeURIComponent(phoneInput)}`);

        if (!res.ok) {
            alert("Lỗi server!");
            return;
        }

        const data = await res.json();

        if (!data.success) {
            resultContainer.innerHTML = `<p style="color:red">${data.message}</p>`;
            return;
        }

        const { orders, bookings } = data;

        let html = `<p style="margin-bottom:10px; font-weight:bold;">Kết quả cho SĐT: ${phoneInput}</p>`;

        // --- HIỂN THỊ ĐƠN HÀNG ---
        if (orders && orders.length > 0) {
            orders.forEach(o => {
                html += `
                    <div style="background:#f0f7ff; padding:10px; border-radius:8px; margin-bottom:10px; border-left:4px solid #007bff;">
                        <div style="display:flex; justify-content:space-between;">
                            <span style="font-weight:bold; color:#007bff;">[Đơn hàng] #${o.orderCode}</span>
                            <span style="font-size:12px; background:#fff; padding:2px 8px; border-radius:10px; border:1px solid #ddd;">${o.status}</span>
                        </div>
                        <p style="font-size:13px; margin-top:5px; color:#555;">
                            ${o.items?.map(i => i.menuItemName).join(', ') || ''}
                        </p>
                    </div>`;
            });
        }

        // --- HIỂN THỊ ĐẶT BÀN ---
        if (bookings && bookings.length > 0) {
            bookings.forEach(b => {
                html += `
                    <div style="background:#fff3cd; padding:10px; border-radius:8px; margin-bottom:10px; border-left:4px solid #ffc107;">
                        <div style="display:flex; justify-content:space-between;">
                            <span style="font-weight:bold; color:#856404;">[Đặt bàn] #${b.bookingCode}</span>
                            <span style="font-size:12px; background:#fff; padding:2px 8px; border-radius:10px; border:1px solid #ddd;">${b.status}</span>
                        </div>
                        <p style="font-size:13px; margin-top:5px; color:#555;">
                            ${b.guestCount} người - ${b.timeSlotLabel}
                        </p>
                    </div>`;
            });
        }

        if ((!orders || orders.length === 0) && (!bookings || bookings.length === 0)) {
            html += `<p style="color:red; text-align:center;">Không tìm thấy dữ liệu!</p>`;
        }

        resultContainer.innerHTML = html;

    } catch (err) {
        console.error(err);
        resultContainer.innerHTML = `<p style="color:red;">Lỗi kết nối server!</p>`;
    }
}

// Hàm tải khung giờ từ backend
async function loadTimeSlots() {
    try {
        const res = await fetch('/Home/GetTimeSlots');
        if (!res.ok) {
            console.error('Failed to load time slots:', res.status);
            return [];
        }
        return await res.json();
    } catch (err) {
        console.error('Error loading time slots:', err);
        return [];
    }
}

// --- 2. Xử lý Modal chức năng ---
async function openModal(action) {
    const modal = document.getElementById("functionModal");
    const title = document.getElementById("modalTitle");
    const body = document.getElementById("modalBody");

    let formHtml = '';
    title.innerText = action.toUpperCase();

    switch (action) {
        case 'đặt bàn':
            title.innerHTML = '<i class="fas fa-chair"></i> Đặt Bàn Ngay';

            // Tải khung giờ từ backend
            const slots = await loadTimeSlots();
            const slotOptions = slots.length > 0
                ? slots.map(s => `<option value="${s.id}">${s.slotName || s.name}</option>`).join('')
                : '<option value="">Không có khung giờ</option>';

            formHtml = `
                <div class="booking-form" style="display:flex; flex-direction:column; gap:15px; text-align:left;">
                    <div class="form-group">
                        <label style="font-weight:bold; font-size:14px;">Họ và tên *</label>
                        <input type="text" id="bookingName" placeholder="Nhập tên của bạn" style="width:100%; padding:10px; border:1px solid #ddd; border-radius:5px;">
                    </div>
                    <div class="form-group">
                        <label style="font-weight:bold; font-size:14px;">Số điện thoại *</label>
                        <input type="tel" id="bookingPhone" placeholder="Ví dụ: 0912xxxxxx" style="width:100%; padding:10px; border:1px solid #ddd; border-radius:5px;">
                    </div>
                    <div style="display:flex; gap:10px;">
                        <div style="flex:1;">
                            <label style="font-weight:bold; font-size:14px;">Ngày đến *</label>
                            <input type="date" id="bookingDate" style="width:100%; padding:10px; border:1px solid #ddd; border-radius:5px;">
                        </div>
                        <div style="flex:1;">
                            <label style="font-weight:bold; font-size:14px;">Khung giờ *</label>
                            <select id="bookingTimeSlot" style="width:100%; padding:10px; border:1px solid #ddd; border-radius:5px;">
                                <option value="">-- Chọn khung giờ --</option>
                                ${slotOptions}
                            </select>
                        </div>
                    </div>
                    <div class="form-group">
                        <label style="font-weight:bold; font-size:14px;">Số lượng người *</label>
                        <input type="number" id="bookingGuests" min="1" max="6" value="1" style="width:100%; padding:10px; border:1px solid #ddd; border-radius:5px;">
                    </div>
                    <div class="form-group">
                        <label style="font-weight:bold; font-size:14px;">Ghi chú</label>
                        <textarea id="bookingNote" placeholder="Yêu cầu đặc biệt (nếu có)..." style="width:100%; padding:10px; border:1px solid #ddd; border-radius:5px; height:60px; resize:none;"></textarea>
                    </div>
                    <button onclick="confirmBooking()" style="padding:12px; background-color:#e44d26; color:white; border:none; border-radius:5px; cursor:pointer; font-weight:bold;">XÁC NHẬN ĐẶT BÀN</button>
                </div>`;
            break;

        case 'đặt món':
            title.innerHTML = '<i class="fas fa-shopping-basket"></i> Đặt Món Trực Tuyến';

            let cartListHtml = cart.length > 0
                ? `<div style="background:#f8f9fa; padding:10px; border-radius:8px; margin-bottom:15px; border: 1px dashed #e44d26;">
                    <strong style="display:block; margin-bottom:5px; font-size:13px;">Đơn hàng (Tối đa 3 món):</strong>
                    ${cart.map((item, index) => `
                        <div style="display:flex; justify-content:space-between; margin-bottom:3px; font-size:13px;">
                            <span>${index + 1}. ${item.name}</span>
                            <span onclick="removeFromCart(${index})" style="color:red; cursor:pointer; font-size:11px;">[Xóa]</span>
                        </div>
                    `).join('')}
                   </div>`
                : `<p style="color:red; text-align:center; padding:10px; font-size:14px;">Giỏ hàng trống! Hãy chọn món ở Menu.</p>`;

            formHtml = `
                <div style="text-align:left; padding-bottom: 20px;">
                    ${cartListHtml}
                    <div style="display:flex; flex-direction:column; gap:10px;"> 
                        <div>
                            <label style="font-weight:bold; font-size:13px;">Họ tên *</label>
                            <input type="text" id="orderName" placeholder="Tên người nhận" style="width:100%; padding:8px; border:1px solid #ddd; border-radius:5px;">
                        </div>
                        <div>
                            <label style="font-weight:bold; font-size:13px;">Số điện thoại *</label>
                            <input type="tel" id="orderPhone" placeholder="SĐT liên lạc" style="width:100%; padding:8px; border:1px solid #ddd; border-radius:5px;">
                        </div>
                        <div>
                            <label style="font-weight:bold; font-size:13px;">Địa chỉ *</label>
                            <textarea id="orderAddress" placeholder="Địa chỉ giao hàng..." style="width:100%; padding:8px; border:1px solid #ddd; border-radius:5px; height:50px; resize: none;"></textarea>
                        </div>
                        <div style="display:flex; gap:10px;"> 
                            <div style="flex:1">
                                <label style="font-weight:bold; font-size:13px;">Thời gian *</label>
                                <input type="datetime-local" id="orderTime" style="width:100%; padding:8px; border:1px solid #ddd; border-radius:5px; font-size:12px;">
                            </div>
                            <div style="flex:1">
                                <label style="font-weight:bold; font-size:13px;">Ghi chú</label>
                                <input type="text" id="orderNote" placeholder="Ớt, hành..." style="width:100%; padding:8px; border:1px solid #ddd; border-radius:5px;">
                            </div>
                        </div>
                        <button onclick="confirmOrder()" style="padding:12px; background-color:#28a745; color:white; border:none; border-radius:5px; cursor:pointer; font-weight:bold; margin-top:5px; width:100%;">GỬI ĐƠN HÀNG</button>
                    </div>
                </div>`;
            break;

        case 'tra cứu':
            title.innerHTML = '<i class="fas fa-search"></i> Tra cứu Đơn hàng & Đặt bàn';
            formHtml = `
                <div style="text-align:left;">
                    <p style="font-size:14px; margin-bottom:15px; color:#666;">Nhập số điện thoại bạn đã dùng để đặt món hoặc đặt bàn.</p>
                    <div style="display:flex; gap:10px; margin-bottom:20px;">
                        <input type="tel" id="searchPhone" placeholder="Số điện thoại của bạn..." style="flex:1; padding:10px; border:1px solid #ddd; border-radius:5px;">
                        <button onclick="findOrder()" style="padding:10px 20px; background:#333; color:white; border:none; border-radius:5px; cursor:pointer;">TÌM</button>
                    </div>
                    <div id="searchResult">
                        <p style="text-align:center; color:#ccc; font-style:italic;">Chưa có dữ liệu tra cứu</p>
                    </div>
                </div>`;
            break;
    }

    body.innerHTML = formHtml;
    modal.style.display = "block";
}

function closeModal() {
    document.getElementById("functionModal").style.display = "none";
}

// Hàm xác nhận đặt bàn (tương tự confirmOrder)
async function confirmBooking() {
    const name = document.getElementById('bookingName').value.trim();
    const phone = document.getElementById('bookingPhone').value.trim();
    const date = document.getElementById('bookingDate').value;
    const timeSlotId = document.getElementById('bookingTimeSlot').value;
    const guests = document.getElementById('bookingGuests').value;
    const note = document.getElementById('bookingNote').value.trim();

    // Validation
    if (!name) {
        alert("Vui lòng nhập họ tên!");
        return;
    }

    if (!phone) {
        alert("Vui lòng nhập số điện thoại!");
        return;
    }

    if (!date) {
        alert("Vui lòng chọn ngày đặt!");
        return;
    }

    if (!timeSlotId) {
        alert("Vui lòng chọn khung giờ!");
        return;
    }

    if (!guests || guests < 1 || guests > 6) {
        alert("Vui lòng nhập số lượng người (1-6)!");
        return;
    }

    const bookingData = {
        customerName: name,
        customerPhone: phone,
        bookingDate: date,
        timeSlotId: parseInt(timeSlotId),
        guestCount: parseInt(guests),
        note: note || null
    };

    try {
        const res = await fetch('/Home/CreateBooking', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(bookingData)
        });

        if (!res.ok) {
            let text;
            try { text = (await res.json()).message ?? res.statusText; }
            catch { text = res.statusText || `HTTP ${res.status}`; }
            alert(`Lỗi server: ${text}`);
            return;
        }

        const data = await res.json();

        if (!data.success) {
            alert(data.message);
            return;
        }

        alert(`✅ Đặt bàn thành công!\nMã đặt bàn: ${data.bookingCode}\nChúng tôi sẽ liên hệ xác nhận sớm nhất.`);
        closeModal();

    } catch (err) {
        console.error(err);
        alert("Lỗi khi gửi yêu cầu đặt bàn! Vui lòng kiểm tra kết nối và thử lại.");
    }
}

let foodData = [];

async function loadMenu() {
    try {
        const res = await fetch('/Home/GetMenuItems');
        if (!res.ok) {
            console.error('Failed to load menu:', res.status, res.statusText);
            return;
        }
        const data = await res.json();

        foodData = data.map(item => ({
            id: item.id,
            name: item.name,
            price: item.price,
            img: item.imageUrl
        }));

        renderFoodGrid(1);
        renderPagination();
    } catch (err) {
        console.error('Error loading menu:', err);
    }
}

const itemsPerPage = 8;
let currentPage = 1;

function renderFoodGrid(page) {
    const grid = document.getElementById('foodGrid');
    if (!grid) return;
    grid.innerHTML = '';

    const startIndex = (page - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    const paginatedItems = foodData.slice(startIndex, endIndex);

    paginatedItems.forEach(food => {
        const cardHtml = `
            <div class="food-card">
                <img src="${food.img}" alt="${food.name}" class="food-img">
                <div class="food-info">
                    <h4 class="food-name">${food.name}</h4>
                    <p class="food-price">${(food.price ?? 0).toLocaleString('vi-VN')}đ</p>
                    <button onclick='addToCart(${JSON.stringify(food)})' style="margin-top:10px; width:100%; padding:8px; background:#28a745; color:white; border:none; border-radius:5px; cursor:pointer; font-size:13px;">
                        <i class="fas fa-plus"></i> Chọn món
                    </button>
                </div>
            </div>
        `;
        grid.innerHTML += cardHtml;
    });
}

function renderPagination() {
    const paginationContainer = document.getElementById('pagination');
    if (!paginationContainer) return;
    paginationContainer.innerHTML = '';
    const totalPages = Math.ceil(foodData.length / itemsPerPage);
    if (totalPages <= 1) return;

    const prevBtn = document.createElement('button');
    prevBtn.className = 'pg-btn';
    prevBtn.innerText = 'Trước';
    prevBtn.disabled = currentPage === 1;
    prevBtn.onclick = () => changePage(currentPage - 1);
    paginationContainer.appendChild(prevBtn);

    for (let i = 1; i <= totalPages; i++) {
        const pageBtn = document.createElement('button');
        pageBtn.className = `pg-btn ${i === currentPage ? 'active' : ''}`;
        pageBtn.innerText = i;
        pageBtn.onclick = () => changePage(i);
        paginationContainer.appendChild(pageBtn);
    }

    const nextBtn = document.createElement('button');
    nextBtn.className = 'pg-btn';
    nextBtn.innerText = 'Sau';
    nextBtn.disabled = currentPage === totalPages;
    nextBtn.onclick = () => changePage(currentPage + 1);
    paginationContainer.appendChild(nextBtn);
}

function changePage(page) {
    currentPage = page;
    renderFoodGrid(currentPage);
    renderPagination();
    document.getElementById('foodGrid').scrollIntoView({ behavior: 'smooth' });
}

window.onload = function () {
    renderFoodGrid(currentPage);
    renderPagination();
    loadMenu();
};