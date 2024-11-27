const apiBaseUrl = "/api/diseaseSymptoms"; // Базовый URL для новой таблицы
let currentPage = 1; // Текущая страница
const itemsPerPage = 10; // Количество записей на странице

function renderDiseaseSymptoms(items, totalItems, currentPage) {
    const container = document.getElementById("disease-symptoms-container");
    container.innerHTML = "";

    if (items.length === 0) {
        container.innerHTML = `No disease symptoms found.`;
        return;
    }

    const table = document.createElement("table");
    const caption = document.createElement("caption");
    caption.innerHTML = `
        Болезни и симптомы
        <a href="javascript:void(0);" onclick="addEmptyRowDiseaseSymptom()" title="Add Item">
            <i class="bi bi-plus-square-fill"></i>
        </a>`;
    table.appendChild(caption);

    const thead = document.createElement("thead");
    thead.innerHTML = `
        <tr>
            <th>Disease</th>
            <th>Symptom</th>
            <th>Actions</th>
        </tr>
    `;
    table.appendChild(thead);

    const tbody = document.createElement("tbody");
    tbody.innerHTML = items.map(item => `
        <tr data-id="${item.id}">
            <td>${item.disease.name}</td>
            <td>${item.symptom.name}</td>
            <td class="actions">
                <a href="javascript:void(0);" onclick="editRowDiseaseSymptom(this)" title="Edit">
                    <i class="bi bi-pencil-fill"></i>
                </a>
                <a href="javascript:void(0);" onclick="showDiseaseSymptomDetails(this)" title="Delete Item">
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

async function loadDiseaseSymptoms(page = 1) {
    try {
        const diseaseFilter = document.getElementById("filter-disease").value || "";
        const symptomFilter = document.getElementById("filter-symptom").value || "";

        const response = await axios.get(`${apiBaseUrl}`, {
            params: {
                page,
                pageSize: itemsPerPage,
                nameDisease: diseaseFilter,
                nameSymptom: symptomFilter,
            },
        });

        const pageResult = response.data;

        // Рендерим данные
        renderDiseaseSymptoms(pageResult.items, pageResult.totalCount, page);
    } catch (error) {
        console.error("Error fetching disease symptoms:", error);
        document.getElementById("disease-symptoms-container").innerHTML =
            `<p>Error loading data. Please try again later.</p>`;
    }
}

function applyDiseaseSymptomFilters() {
    currentPage = 1; // Сбрасываем на первую страницу
    loadDiseaseSymptoms(currentPage);
}
function renderPagination(totalItems, currentPage) {
    const container = document.getElementById("disease-symptoms-container");

    const totalPages = Math.ceil(totalItems / itemsPerPage);

    const paginationDiv = document.createElement("div");
    paginationDiv.style.textAlign = "center";
    paginationDiv.style.marginTop = "10px";

    // Кнопка для перехода на предыдущую страницу
    const prevButton = document.createElement("button");
    prevButton.innerText = "←";
    prevButton.style.margin = "0 5px";
    prevButton.style.padding = "5px 10px";
    prevButton.style.background = currentPage > 1 ? "#3498DB" : "#ccc";
    prevButton.style.color = currentPage > 1 ? "#fff" : "#666";
    prevButton.style.border = "1px solid #ccc";
    prevButton.style.borderRadius = "4px";
    prevButton.style.cursor = currentPage > 1 ? "pointer" : "not-allowed";
    prevButton.onclick = () => {
        if (currentPage > 1) loadDiseaseSymptoms(currentPage - 1);
    };

    paginationDiv.appendChild(prevButton);

    // Текущая страница и поле для ввода номера страницы
    const currentPageText = document.createElement("span");
    currentPageText.innerText = `Page ${currentPage} of ${totalPages}`;
    currentPageText.style.margin = "0 10px";
    paginationDiv.appendChild(currentPageText);

    const pageInput = document.createElement("input");
    pageInput.type = "number";
    pageInput.min = 1;
    pageInput.max = totalPages;
    pageInput.value = currentPage;
    pageInput.style.width = "50px";
    pageInput.style.margin = "0 5px";
    pageInput.onchange = () => {
        const inputPage = parseInt(pageInput.value, 10);
        if (!isNaN(inputPage)) {
            if (inputPage < 1) loadDiseaseSymptoms(1);
            else if (inputPage > totalPages) loadDiseaseSymptoms(totalPages);
            else loadDiseaseSymptoms(inputPage);
        }
    };
    paginationDiv.appendChild(pageInput);

    // Кнопка для перехода на следующую страницу
    const nextButton = document.createElement("button");
    nextButton.innerText = "→";
    nextButton.style.margin = "0 5px";
    nextButton.style.padding = "5px 10px";
    nextButton.style.background = currentPage < totalPages ? "#3498DB" : "#ccc";
    nextButton.style.color = currentPage < totalPages ? "#fff" : "#666";
    nextButton.style.border = "1px solid #ccc";
    nextButton.style.borderRadius = "4px";
    nextButton.style.cursor = currentPage < totalPages ? "pointer" : "not-allowed";
    nextButton.onclick = () => {
        if (currentPage < totalPages) loadDiseaseSymptoms(currentPage + 1);
    };

    paginationDiv.appendChild(nextButton);
    container.appendChild(paginationDiv);
}

// Инициализация
loadDiseaseSymptoms();