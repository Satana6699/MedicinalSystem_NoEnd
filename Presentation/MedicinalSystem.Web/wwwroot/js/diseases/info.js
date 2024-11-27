function info(deleteButton)
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("modal-info");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = 
    `
        <h3>Are you sure you want to delete the disease?</h3>
        <p><strong>Name:</strong> ${row.cells[0].innerText}</p>
        <p><strong>Duration:</strong> ${row.cells[1].innerText}</p>
        <p><strong>Symptoms:</strong> ${row.cells[2].innerText}</p>
        <p><strong>Consequences:</strong> ${row.cells[3].innerText}</p>
        <button onclick="closeModal()">Close</button>
        <button onclick="deleteRow('${id}')">Delete</button>
    `;

    modal.style.display = "block";
}

async function deleteRow(id)
{
    try {
        const response = await axios.delete(`${apiBaseUrl}/${id}`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });
        if (response.status === 204) {
            const row = document.querySelector(`tr[data-id="${id}"]`);
            if (row) row.remove(); // Удаляем строку из таблицы
            alert("Disease has been deleted successfully.");
        } else {
            alert("Failed to delete disease.");
        }
    } catch (error) {
        console.error("Error deleting disease:", error);
        if (error.response) {
            // Ответ от сервера с ошибкой
            alert(`Server error: ${error.response.status} - ${error.response.statusText}`);
        } else {
            // Ошибка запроса (например, нет соединения)
            alert("Network error: Failed to delete disease.");
        }
    }

    closeModal()
}

function closeModal()
{
    const modal = document.getElementById("modal-info");
    modal.style.display = "none";
}