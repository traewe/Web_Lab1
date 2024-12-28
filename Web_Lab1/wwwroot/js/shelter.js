// Функція для отримання притулків із API
async function getShelters() {
    try {
        const response = await fetch('/api/DogShelter');
        if (!response.ok) {
            throw new Error('Failed to fetch shelters');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching shelters:', error);
        return []; // Повернути порожній масив у разі помилки
    }
}

// Функція для відображення списку притулків
async function displayShelters() {
    const shelters = await getShelters();
    const shelterListDiv = document.getElementById("shelter-list");
    shelterListDiv.innerHTML = ''; // Очистити перед новим виведенням

    if (shelters.length === 0) {
        shelterListDiv.textContent = 'No shelters available or an error occurred.';
        return;
    }

    shelters.forEach(shelter => {
        // Створити елемент для притулку
        const shelterItem = document.createElement("div");
        shelterItem.innerHTML = `
            <h3>Shelter: ${shelter.name}</h3>
            <p>Address: ${shelter.address}</p>
            <p>Contact Number: ${shelter.contactNumber}</p>
        `;

        // Створити список собак для цього притулку
        const dogList = document.createElement("ul");
        if (shelter.dogs && shelter.dogs.length > 0) {
            shelter.dogs.forEach(dog => {
                const dogItem = document.createElement("li");
                dogItem.textContent = `Breed: ${dog.breed}, Name: ${dog.name}, Age: ${dog.age}`;
                dogList.appendChild(dogItem);
            });
        } else {
            const noDogs = document.createElement("p");
            noDogs.textContent = "No dogs available in this shelter.";
            shelterItem.appendChild(noDogs);
        }

        // Додати список собак до елемента притулку
        shelterItem.appendChild(dogList);
        shelterListDiv.appendChild(shelterItem);
    });
}
