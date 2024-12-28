async function getOptions() {
    try {
        const response = await fetch('/api/Options');
        if (!response.ok) {
            throw new Error('Failed to fetch options');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching options:', error);
        return null;
    }
}

async function displayOptions() {
    const data = await getOptions();
    const optionListDiv = document.getElementById("option-list");
    optionListDiv.innerHTML = '';

    if (!data || !data.monitor) {
        optionListDiv.textContent = 'No options available or an error occurred.';
        return;
    }

    const monitor = data.monitor;

    // Відобразити дані монітора
    const monitorItem = document.createElement("div");
    monitorItem.innerHTML = `
        <p>Phone Number: ${monitor.phoneNumber}</p>
        <p>Email: ${monitor.email}</p>
    `;
    optionListDiv.appendChild(monitorItem);
}
