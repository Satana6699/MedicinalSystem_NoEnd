async function loadDiseases()
{
    try 
    {
        const response = await axios.get(apiBaseUrl);
        renderDiseases(response.data);
    } 
    catch (error) 
    {
        console.error("Error fetching diseases:", error);
        document.getElementById("diseases-container").innerHTML =
            `<p>Error loading diseases. Please try again later.</p>`;
    }
}

loadDiseases();