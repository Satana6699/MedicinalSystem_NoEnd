function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки
    newRow.innerHTML = `
        <td contenteditable="false"></td>
        <td contenteditable="false"></td>
        <td contenteditable="false"></td>
        <td data-field="manufacturer" data-manufacturer-id="0"></td>
        <td contenteditable="false"></td>
        <td contenteditable="false"></td>

        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRow(this)" title="Save">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRow(this)" title="Cancel">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;
    
    const cells = Array.from(newRow.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    newRow.classList.add('editing');
    newRow.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));

    cells.forEach(cell => {
        if (cell.dataset.field === "manufacturer") {
            cell.addEventListener('click', () => openSelectModal(cell));
        }
    });

    cells.forEach(cell => cell.setAttribute('contenteditable', 'true')); // Только данные можно редактировать

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}

async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));

    // Собираем данные из строки
    const updatedData = {
        name: cells[0].innerText.trim(),
        indications: cells[1].innerText.trim(),
        contraindications: cells[2].innerText.trim(),
        manufacturerId: cells[3].dataset.manufacturerId,
        packaging: cells[4].innerText.trim(),
        dosage: cells[5].innerText.trim(),
    };
    // Проверяем заполненность поля
    if (!updatedData && !updatedData.Id) {
        alert("Не все поля заполнены");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, updatedData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        if (response.status === 201) {
            alert("Новый пропорат создан успешно!");
        }

        const price = promptForPrice();
        const createPrice = {
            medicineId: response.data.id,
            price: price,
            date: new Date(),
        }

        const response2 = await axios.post('/api/medicinePrices', createPrice, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });
        
        if (response2.status === 201) {
            alert("Цена установлена успешно!");
            location.reload();
        }
        else {
            throw new Error("Ошбика создания данных");
        }
    } catch (error) {
        console.error("Ошбика создание данных:", error);
        alert("Ошбика при создании данных. Потворите попытку позже");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}

function promptForPrice() {
    let price = 9999999;

    // Запрашиваем ввод до тех пор, пока не введут корректное число
    do {
        const input = prompt("Введите цену препарата:");
        if (input === null) {
            // Если пользователь нажал "Отмена", возвращаем null
            return null;
        }

        // Проверяем, чтобы ввод содержал только цифры
        if (/^\d+$/.test(input)) {
            price = parseInt(input, 10); // Преобразуем в число
        } else {
            alert("Некорректный ввод! Пожалуйста, введите только цифры.");
        }
    } while (price === null);

    return price;
}