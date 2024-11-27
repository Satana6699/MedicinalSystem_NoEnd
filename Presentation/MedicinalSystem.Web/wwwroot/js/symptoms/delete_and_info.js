function delete_and_infoSymptom(deleteButton)
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("symptomModal");
    const modalContent = modal.querySelector(".modal-content");

    modalContent.innerHTML = 
    `
        <h3>Are you sure you want to delete the symptom?</h3>
        <p><strong>Симптом:</strong> ${row.cells[0].innerText}</p>
        <button onclick="closeModalSymptom()">Close</button>
        <button onclick="deleteSymptom('${id}')">Delete</button>
    `;

    modal.style.display = "block";
}

async function deleteSymptom(id)
{
    try {
        const response = await axios.delete(`${apiBaseUrl}/${id}`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token') }`,
            },
        });
        if (response.status === 204) {
            const row = document.querySelector(`tr[data-id="${id}"]`);
            if (row) row.remove(); // Удаляем строку из таблицы
            alert("Symptom has been deleted successfully.");
        } else {
            alert("Failed to delete Symptom.");
        }
    } catch (error) {
        console.error("Error deleting Symptom:", error);
        if (error.response) {
            // Ответ от сервера с ошибкой
            alert(`Server error: ${error.response.status} - ${error.response.statusText}`);
        } else {
            // Ошибка запроса (например, нет соединения)
            alert("Network error: Failed to delete Symptom.");
        }
    }

    // Закрываем модальное окно после удаления
    const modal = document.getElementById("symptomModal");
    modal.style.display = "none";
}

function closeModalSymptom()
{
    const modal = document.getElementById("symptomModal");
    modal.style.display = "none";
}