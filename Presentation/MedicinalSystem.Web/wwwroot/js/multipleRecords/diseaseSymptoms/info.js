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
    const disease = item.data.disease;
    const symptom = item.data.symptom;
    modalContent.innerHTML = `
        <h3>Детали болезни и симптома</h3>
        <p><strong>Болезнь:</strong> ${disease.name}</p>
        <p><strong>Продолжительность болезни:</strong> ${disease.duration}</p>
        <p><strong>Симптом:</strong> ${disease.symptoms}</p>
        <p><strong>Последствия:</strong> ${disease.consequences}</p>
        <p><strong>Симптом:</strong> ${symptom.name}</p>
        <button onclick=\"closeModal()\">Close</button>
        <button onclick=\"deleteRow('${row.dataset.id}')\">Delete</button>
    `;

    modal.style.display = "block";
}