function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRow(this)" title="Save">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRow(this)" title="Cancel">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}
async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = row.querySelectorAll("td[contenteditable]");
    
    // Собираем данные из строки
    const newDisease = {
        name: cells[0].innerText.trim(),
        duration: cells[1].innerText.trim(),
        symptoms: cells[2].innerText.trim(),
        consequences: cells[3].innerText.trim(),
    };

    // Проверяем заполненность полей
    if (!newDisease.name || !newDisease.duration || !newDisease.symptoms || !newDisease.consequences) {
        alert("All fields must be filled.");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, newDisease, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        if (response.status === 201) {
            alert("Disease created successfully!");

            // Обновляем строку с новыми данными
            row.dataset.id = response.data.id; // Устанавливаем ID, полученный от сервера
            row.innerHTML = `
                <td style="padding: 8px;" contenteditable="false">${response.data.name}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.duration}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.symptoms}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.consequences}</td>
                <td style="padding: 8px;">
                    <a href="javascript:void(0);" onclick="editRowDisease(this)" title="Edit">
                        <i class="bi bi-pencil-fill"></i>
                    </a>
                    <a href="javascript:void(0);" onclick="delete_and_infoDisease(this)" title="Delete Item">
                        <i class="bi bi-eye-fill"></i>
                    </a>
                </td>
            `;
        } else {
            throw new Error("Failed to create disease.");
        }
    } catch (error) {
        console.error("Error creating disease:", error);
        alert("Failed to create disease. Please try again.");

        // Удаляем строку при ошибке
        row.remove();
    }
}
function cancelNewRowDisease(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}