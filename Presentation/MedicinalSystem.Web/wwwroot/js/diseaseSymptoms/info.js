async function showDiseaseSymptomDetails(deleteButton) {
    const row = deleteButton.closest('tr');
    const symptom = await axios.get("/api/diseaseSymptoms/" +  row.dataset.id);
    const modal = document.getElementById("diseaseSymptomModal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = `
        <h3>Детали болезни и симптома</h3>
        <p><strong>Болезнь:</strong> ${symptom.data.disease.name}</p>
        <p><strong>Продолжительность болезни:</strong> ${symptom.data.disease.duration}</p>
        <p><strong>Симптом:</strong> ${symptom.data.disease.symptoms}</p>
        <p><strong>Последствия:</strong> ${symptom.data.disease.consequences}</p>
        <p><strong>Симптом:</strong> ${symptom.data.symptom.name}</p>
        <button onclick=\"closeDiseaseSymptomModal()\">Close</button>
        <button onclick=\"deleteDiseaseSymptom('${row.dataset.id}')\">Delete</button>
    `;

    modal.style.display = "block";
}

async function deleteDiseaseSymptom(id) {
    try {
        const response = await axios.delete(`${apiBaseUrl}/disease-symptoms/${id}`);
        if (response.status === 204) {
            const row = document.querySelector(`tr[data-id="${id}"]`);
            if (row) row.remove(); // Удаляем строку из таблицы
            alert("The disease-symptom association has been deleted successfully.");
        } else {
            alert("Failed to delete the association.");
        }
    } catch (error) {
        console.error("Error deleting the association:", error);
        if (error.response) {
            // Ответ от сервера с ошибкой
            alert(`Server error: ${error.response.status} - ${error.response.statusText}`);
        } else {
            // Ошибка запроса (например, нет соединения)
            alert("Network error: Failed to delete the association.");
        }
    }

    // Закрываем модальное окно после удаления
    const modal = document.getElementById("diseaseSymptomModal");
    modal.style.display = "none";
}

function closeDiseaseSymptomModal() {
    const modal = document.getElementById("diseaseSymptomModal");
    modal.style.display = "none";
}
