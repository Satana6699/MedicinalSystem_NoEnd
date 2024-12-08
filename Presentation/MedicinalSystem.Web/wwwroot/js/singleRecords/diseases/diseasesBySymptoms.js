function openSelectModal(cell) {
    const type = 'symptom';

    // Удаляем все открытые модальные окна, если они есть
    const existingModal = document.querySelector('.modal-list');
    if (existingModal) {
        existingModal.remove();
    }

    // Создаем новое модальное окно
    const modal = document.createElement('div');
    modal.classList.add('modal-list');
    modal.innerHTML = `
            <div class="modal-list-content">
                <div class="modal-list-header">
                    <span class="close">&times;</span>
                    <h2>Select ${type}</h2>
                </div>
                <div class="modal-list-body">
                    <table id="select-table">
                        <!-- Данные для выбора загружаются динамически -->
                    </table>
                </div>
            </div>
        `;
    document.body.appendChild(modal);
    // Обработчик закрытия модального окна
    modal.querySelector('.close').onclick = () => {
        // Сбросить значения в контейнере
        cell.dataset.symptomId = 0;
        cell.innerText = 'Симптом';

        modal.remove();
    };

    // Позиционируем модальное окно под ячейкой
    const cellRect = cell.getBoundingClientRect();
    modal.style.left = `${cellRect.left}px`;
    modal.style.top = `${cellRect.bottom + window.scrollY}px`; // Учитываем прокрутку страницы

    loadSelectData(type, cell);
}

async function loadSelectData(type, cell) {
    try {
        const response = await axios.get(`${apiBaseUrl}/${type}s`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        const table = document.getElementById('select-table');
        table.innerHTML = '';

        response.data.forEach(item => {
            const row = document.createElement('tr');
            row.dataset.id = item.id;
            row.innerHTML = `<td>${item.name}</td>`;
            row.onclick = () => selectItem(item, cell, type);
            table.appendChild(row);
        });

    } catch (error) {
        console.error("Error loading select data:", error);
        alert("Failed to load data. Please try again.");
    }
}

function selectItem(item, cell, type) {
    if (type === 'symptom') {
        cell.dataset.symptomId = item.id;
        cell.innerText = item.name;
    }

    const modal = document.querySelector('.modal-list');
    if (modal) modal.remove();
}


async function collectSymptomIds(page = 1) {
    const symptomIds = [];

    // Ищем все элементы с классом .guid-cell
    document.querySelectorAll('.guid-cell').forEach(function (cell) {
        const symptomId = cell.getAttribute('data-symptom-id');

        // Добавляем в массив только те ID, которые не равны 0
        if (symptomId !== '0') {
            symptomIds.push(symptomId);
        }
    });
    try {
        let response;
        if (symptomIds && symptomIds.length > 0) {
            const symptomIdsString = symptomIds.join('&symptomIds=');

            // Строка запроса
            const url = `${apiBaseUrl}/by-symptoms?page=${page}&pageSize=${itemsPerPage}&symptomIds=${symptomIdsString}`;

            // Выполняем запрос
            response = await axios.get(url, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
            });

            //response = await axios.get(apiBaseUrl + "/" + "by-symptoms", {
            //    params: {
            //        page: page,
            //        pageSize: itemsPerPage,
            //        symptomIds: symptomIdsString,
            //    },
            //    headers: {
            //        Authorization: `Bearer ${localStorage.getItem('token')}`,
            //    },
            //});
        } else {
            response = await axios.get(apiBaseUrl, {
                params: {
                    page: page,
                    pageSize: itemsPerPage,
                    name: "",
                },
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
            });
        }
        


        // Создание переменных для таблицы
        const itemsLength = response.data.items.length;
        const totalCount = response.data.totalCount;
        const tableTitle = "Болезни";
        const tableHead = `
            <tr>
                <th style="padding: 8px; text-align: left;">Имя</th>
                <th style="padding: 8px; text-align: left;">Продолжительность</th>
                <th style="padding: 8px; text-align: left;">Первичные симптомы</th>
                <th style="padding: 8px; text-align: left;">Последстивя</th>
                <th style="padding: 8px; text-align: left;">Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td style="padding: 8px;" contenteditable="false">${item.name}</td>
                <td style="padding: 8px;" contenteditable="false">${item.duration}</td>
                <td style="padding: 8px;" contenteditable="false">${item.symptoms}</td>
                <td style="padding: 8px;" contenteditable="false">${item.consequences}</td>
                <td style="padding: 8px;">
                    <a class="edit-buttons" href="javascript:void(0);" onclick="editRow(this)" title="Edit">
                        <i class="bi bi-pencil-fill"></i>
                    </a>
                    <a href="javascript:void(0);" onclick="info(this)" title="Delete Item">
                        <i class="bi bi-eye-fill"></i>
                    </a>
                </td>
            </tr>
        `).join('');

        // Создание таблицы
        createTable(itemsLength, totalCount, page, tableTitle, tableHead, tableBody, collectSymptomIds);
    } catch (error) {
        ERROR(error);
    }   
}