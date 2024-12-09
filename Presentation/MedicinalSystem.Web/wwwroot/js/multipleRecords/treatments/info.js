async function info(deleteButton) {
    const row = deleteButton.closest('tr');
    const item = await axios.get(apiBaseUrl + "/" + row.dataset.id, {
        headers:
        {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
    });
    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");
    modalContent.innerHTML = `
        <h3>Детали пациента</h3>
        <p><strong>Болезнь: :</strong> ${item.data.disease.name}</p>
        <p><strong>Препорат:</strong> ${item.data.medicine.name}</p>
        <p><strong>Дозировка:</strong> ${item.data.dosage}</p>
        <p><strong>Как долго принимать:</strong> ${item.data.durationDays}д.</p>
        <p><strong>Через какие промежутки времени принимать:</strong> ${item.data.intervalHours}ч.</p>
        <p><strong>Инструкция к применению:</strong> ${item.data.instructions}</p>
        <button onclick=\"closeModal()\">Close</button>
        <button onclick=\"deleteRow('${row.dataset.id}')\">Delete</button>
    `;

    modal.style.display = "block";
}