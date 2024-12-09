function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    const newRow = document.createElement("tr");
    newRow.innerHTML = `
    <td colspan="3" style="text-align: center;">
        <a href="/Tables/MultipleRecords/Medicines/">Добавить новый препарат</a>
    </td>
    <td style="padding: 8px;">
        <a href="javascript:void(0);" onclick="cancelNewRow(this)" title="Cancel">
            <i class="bi bi-x-circle-fill"></i>
        </a>
    </td>
`;
    table.prepend(newRow);
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove();
}