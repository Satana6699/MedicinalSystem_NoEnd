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
        <h3>${item.data.name}</h3>
        <p><strong>Препорат: </strong> ${item.data.name}</p>
        <p><strong>Показания: </strong> ${item.data.indications}</p>
        <p><strong>Противопоказания: </strong> ${item.data.contraindications}</p>
        <p><strong>Производитель: </strong> ${item.data.manufacturer.name}</p>
        <p><strong>Упаковка: </strong> ${item.data.packaging}</p>
        <p><strong>Дозировка: </strong> ${item.data.dosage}</p>
        <button onclick=\"closeModal()\">Close</button>
        <button onclick=\"deleteRow('${row.dataset.id}')\">Delete</button>
    `;

    modal.style.display = "block";
}