
function editRow(editButton) 
{
    const row = editButton.closest('tr');
    const cells = row.querySelectorAll('td[contenteditable]');
    const isEditing = row.classList.contains('editing');

    if (isEditing) 
    {
        // Save changes
        const id = row.dataset.id;
        const updatedData = 
        {
            id: id,
            name: cells[0].innerText.trim(),
            duration: cells[1].innerText.trim(),
            symptoms: cells[2].innerText.trim(),
            consequences: cells[3].innerText.trim()
        };

        saveChanges(id, updatedData, row);
    } 
    else 
    {
        // Start editing
        row.classList.add('editing');
        cells.forEach(cell => cell.setAttribute('contenteditable', 'true'));
        editButton.innerHTML = '<i class="bi bi-check-circle-fill"></i>'; // Change to save icon
        editButton.title = "Save";
    }
}

async function saveChanges(id, updatedData, row) 
{
    try 
    {
        await axios.put(`${apiBaseUrl}/${id}`, updatedData);
        row.classList.remove('editing');
        const cells = row.querySelectorAll('td[contenteditable]');
        cells.forEach(cell => cell.setAttribute('contenteditable', 'false'));

        const editButton = row.querySelector('a[title="Save"]');
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>'; // Change back to edit icon
        editButton.title = "Edit";
    } 
    catch (error) 
    {
        console.error("Error saving changes:", error);
        alert("Failed to save changes. Please try again.");
    }
}