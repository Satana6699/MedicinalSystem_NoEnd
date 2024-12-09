function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `
        <td data-field="familyMember" data-family-member-id="0"></td>
        <td data-field="disease" data-disease-id="0"></td>
        <td date-str=${new Date().toISOString() }>${ISODateInBaseDate(new Date())}</td>
        <td data-field="status">
            ${false ? 'Рецепт использован' : 'Рецепт не использован'}
        </td>
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
        if (cell.dataset.field === "disease" || cell.dataset.field === "familyMember") {
            cell.addEventListener('click', () => openSelectModal(cell));
        }
    });

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}
async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));

    // Собираем данные из строки
    const id = row.dataset.id;
    const newDate = new Date().toISOString();
    const updatedData = {
        familyMemberId: cells[0].dataset.familyMemberId,
        diseaseId: cells[1].dataset.diseaseId,
        date: newDate,
        status: cells[3].dataset.status === "true"
    };

    // Проверяем заполненность поля
    if (!updatedData.diseaseId && !updatedData.familyMemberId) {
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
            alert("familyMember created successfully!");

            //// Обновляем строку с новыми данными
            //row.dataset.id = response.data.id; // Устанавливаем ID, полученный от сервера
            //row.innerHTML = `
            //<td data-field="disease" data-disease-id="${response.dataset.id}">${item.disease.name}</td>
            //<td data-field="familyMember" data-familyMember-id="${response.dataset.id}">${item.familyMember.name}</td>
            //    <td style="padding: 8px;">
            //        <a href="javascript:void(0);" onclick="editRow(this)" title="Edit">
            //            <i class="bi bi-pencil-fill"></i>
            //        </a>
            //        <a href="javascript:void(0);" onclick="info(this)" title="Delete Item">
            //            <i class="bi bi-eye-fill"></i>
            //        </a>
            //    </td>
            //`;

            location.reload();

        } else {
            throw new Error("Failed to create familyMember.");
        }
    } catch (error) {
        console.error("Error creating familyMember:", error);
        alert("Failed to create familyMember. Please try again.");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}