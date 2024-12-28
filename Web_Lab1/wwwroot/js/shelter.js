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
            <p>Id: ${shelter.id}</p>
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

async function getShelterById(id) {
    try {
        const response = await fetch(`/api/DogShelter/${id}`);
        if (!response.ok) {
            throw new Error('Shelter not found');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching shelter by ID:', error);
        return null; // Повертаємо null, якщо собака не знайдена
    }
}

// Функція для отримання собаки за іменем і породою з API
async function getShelterByNameAndAddress(name, address) {
    try {
        const response = await fetch(`/api/DogShelter/${name}/${address}`);
        if (!response.ok) {
            throw new Error('Shelter not found');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching shelter by name and address:', error);
        return null; // Повертаємо null, якщо собака не знайдена
    }
}

// Функція для відображення собаки за ID
async function displayShelterById() {
    const id = document.getElementById('shelter-id').value;
    if (!id) {
        alert("Please enter a shelter ID.");
        return;
    }

    const shelter = await getShelterById(id);
    const shelterDiv = document.getElementById('shelter-by-id');
    shelterDiv.innerHTML = ''; // Очищаємо перед відображенням нової інформації

    if (shelter) {
        shelterDiv.innerHTML = `
            <p>Id: ${shelter.id}</p>
            <p>Shelter: ${shelter.name}</p>
            <p>Address: ${shelter.address}</p>
            <p>Contact Number: ${shelter.contactNumber}</p>
        `;
    } else {
        shelterDiv.textContent = 'Shelter not found or an error occurred.';
    }
}

// Функція для відображення собаки за іменем та породою
async function displayShelterByNameAndAddress() {
    const name = document.getElementById('shelter-name').value;
    const address = document.getElementById('shelter-address').value;

    if (!name || !address) {
        alert("Please enter both name and address.");
        return;
    }

    const shelter = await getShelterByNameAndAddress(name, address);
    const shelterDiv = document.getElementById('shelter-by-name-and-address');
    shelterDiv.innerHTML = ''; // Очищаємо перед відображенням нової інформації

    if (shelter) {
        shelterDiv.innerHTML = `
            <p>Id: ${shelter.id}</p>
            <p>Shelter: ${shelter.name}</p>
            <p>Address: ${shelter.address}</p>
            <p>Contact Number: ${shelter.contactNumber}</p>
        `;
    } else {
        shelterDiv.textContent = 'Shelter not found or an error occurred.';
    }
}

async function addShelter(event) {
    event.preventDefault(); // Запобігаємо перезавантаженню сторінки після натискання на кнопку "Submit"

    // Збираємо дані з форми
    const name = document.getElementById('shelter-name').value;
    const address = document.getElementById('shelter-address').value;
    const contactNumber = document.getElementById('shelter-contact').value;

    // Перевірка на обов'язкові поля
    if (!name || !address || !contactNumber) {
        alert("Please fill in all required fields.");
        return;
    }

    // Створюємо об'єкт притулку
    const shelterData = {
        name: name,
        address: address,
        contactNumber: contactNumber
    };

    // Відправка даних на сервер через POST-запит
    try {
        const response = await fetch('/api/DogShelter', {
            method: 'POST',
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            },
            body: JSON.stringify(shelterData)
        })
            .then((response) => response.json())
            .then((json) => console.log(json));
    } catch (error) {
        console.error('Error adding shelter:', error);
    }
}

async function deleteShelter() {
    const shelterId = document.getElementById('delete-shelter-id').value;

    if (!shelterId) {
        alert("Please enter a shelter ID.");
        return;
    }

    try {
        const response = await fetch(`/api/DogShelter/${shelterId}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            document.getElementById('delete-shelter-response').textContent = `Shelter with ID ${shelterId} deleted successfully.`;
        } else {
            const result = await response.json();
            document.getElementById('delete-shelter-response').textContent = `Error: ${result.message || 'Failed to delete shelter.'}`;
        }
    } catch (error) {
        console.error('Error deleting shelter:', error);
        document.getElementById('delete-shelter-response').textContent = 'Error deleting shelter.';
    }
}

async function updateShelter(event) {
    event.preventDefault();

    const shelterId = document.getElementById('update-shelter-id').value;
    const name = document.getElementById('update-shelter-name').value;
    const address = document.getElementById('update-shelter-address').value;
    const contactNumber = document.getElementById('update-shelter-contact').value;

    const shelterData = {
        Name: name,
        Address: address,
        ContactNumber: contactNumber
    };

    try {
        const response = await fetch(`/api/DogShelter/${shelterId}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(shelterData)
        });
    } catch (error) {
        console.error('Error updating shelter:', error);
    }
}