async function getCities() {
    try {
        const response = await fetch('/api/City/memory');
        if (!response.ok) {
            throw new Error('Failed to fetch cities');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching cities:', error);
        return [];
    }
}

async function displayCities() {
    const cities = await getCities();
    const cityListDiv = document.getElementById("city-list");
    cityListDiv.innerHTML = '';

    if (cities.length === 0) {
        cityListDiv.textContent = 'No cities available or an error occurred.';
        return;
    }

    cities.forEach(city => {
        const cityItem = document.createElement("div");
        cityItem.textContent = `Name: ${city.name}, Number of stray dogs: ${city.numberOfStrayDogs}.`;
        cityListDiv.appendChild(cityItem);
    });
}
