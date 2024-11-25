const apiBaseUrl = "/api/symptoms";
let currentPage = 1; // Текущая страница
const itemsPerPage = 10; // Количество записей на странице

function renderSymptoms(items, totalItems, currentPage) {
    const container = document.getElementById("symptoms-container");
    container.innerHTML = "";

    console.log('symptoms length:', items.length); // Проверка длины массива

    if (items.length === 0) {
        container.innerHTML = `No symptoms found.`;
        return;
    }

    const table = document.createElement("table");
    const caption = document.createElement("caption");
    caption.innerHTML = `
        Symptom List
        <a href="javascript:void(0);" onclick="addEmptyRowSymptom()" title="Add Item">
            <i class="bi bi-plus-square-fill"></i>
        </a>`;
    table.appendChild(caption);

    const thead = document.createElement("thead");
    thead.innerHTML = `
        <tr>
            <th>Symptom</th>
            <th>Actions</th>
        </tr>
    `;
    table.appendChild(thead);

    const tbody = document.createElement("tbody");
    tbody.innerHTML = items.map(item => `
        <tr data-id="${item.id}">
            <td contenteditable="false">${item.name}</td>
            <td class="actions">
                <a href="javascript:void(0);" onclick="editRowSymptom(this)" title="Edit">
                    <i class="bi bi-pencil-fill"></i>
                </a>
                <a href="javascript:void(0);" onclick="delete_and_infoSymptom(this)" title="Delete Item">
                    <i class="bi bi-eye-fill"></i>
                </a>
            </td>
        </tr>
    `).join('');

    table.appendChild(tbody);

    container.appendChild(table);

    // Рендерим пагинацию
    renderPagination(totalItems, currentPage);
}

function renderPagination(totalItems, currentPage) {
    const container = document.getElementById("symptoms-container");
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    const paginationDiv = document.createElement("div");
    paginationDiv.style.textAlign = "center";
    paginationDiv.style.marginTop = "10px";

    for (let page = 1; page <= totalPages; page++) {
        const pageButton = document.createElement("button");
        pageButton.style.margin = "0 5px";
        pageButton.style.padding = "5px 10px";
        pageButton.style.background = page === currentPage ? "#2e7d32" : "#f0f0f0";
        pageButton.style.color = page === currentPage ? "#fff" : "#000";
        pageButton.style.border = "1px solid #ccc";
        pageButton.style.borderRadius = "4px";
        pageButton.style.cursor = "pointer";

        pageButton.innerText = page;
        pageButton.onclick = () => loadSymptoms(page);

        paginationDiv.appendChild(pageButton);
    }

    container.appendChild(paginationDiv);
}


async function loadSymptoms(page = 1) {
    try {
        const response = await axios.get(`${apiBaseUrl}?page=${page}&pageSize=${itemsPerPage}`);
        const pageResult = response.data;

        // Рендерим симптомы и пагинацию
        renderSymptoms(pageResult.items, pageResult.totalCount, page);
    } catch (error) {
        console.error("Error fetching symptoms:", error);
        document.getElementById("symptoms-container").innerHTML =
            `<p>Error loading symptoms. Please try again later.</p>`;
    }
}

// Инициализация
loadSymptoms();