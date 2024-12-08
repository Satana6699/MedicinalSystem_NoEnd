function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `

        <td data-field="disease" data-disease-id="0"></td>
        <td data-field="medicine" data-medicine-id="0"></td>
        <td contenteditable="false"></td>
        <td contenteditable="false" oninput="validateInput(this)"></td>
        <td contenteditable="false" oninput="validateInput(this)"></td>
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
    cells.forEach(cell => cell.setAttribute('contenteditable', 'true')); // Только данные можно редактировать

    cells.forEach(cell => {
        if (cell.dataset.field === "disease" || cell.dataset.field === "medicine") {
            cell.addEventListener('click', () => openSelectModal(cell));
        }
    });

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}
async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));

    const id = row.dataset.id;
    const updatedData = {
        diseaseId: cells[0].dataset.diseaseId,
        medicineId: cells[1].dataset.medicineId,
        dosage: cells[2].innerText.trim(),
        durationDays: cells[3].innerText.trim(),
        intervalHours: cells[4].innerText.trim(),
        instructions: cells[5].innerText.trim()
    };

    // Проверяем заполненность поля
    if (!updatedData.diseaseId && !updatedData.medicineId && !updatedData.dosage && !updatedData.durationDays
        && !updatedData.instructions && !updatedData.instructions) {
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
            alert("medicine created successfully!");

            
            location.reload();

        } else {
            throw new Error("Failed to create medicine.");
        }
    } catch (error) {
        console.error("Error creating medicine:", error);
        alert("Failed to create medicine. Please try again.");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}