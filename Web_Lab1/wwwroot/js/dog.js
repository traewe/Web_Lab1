// Функція для отримання собак із API
async function getDogs() {
    try {
        const response = await fetch('/api/Dog');
        if (!response.ok) {
            throw new Error('Failed to fetch dogs');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching dogs:', error);
        return []; // Повернути порожній масив у разі помилки
    }
}

// Функція для відображення списку собак
async function displayDogs() {
    const dogs = await getDogs();
    const dogListDiv = document.getElementById("dog-list");
    dogListDiv.innerHTML = ''; // Очистити перед новим виведенням

    if (dogs.length === 0) {
        dogListDiv.textContent = 'No dogs available or an error occurred.';
        return;
    }

    dogs.forEach(dog => {
        const dogItem = document.createElement("div");
        dogItem.textContent = `Id: ${dog.id}, Name: ${dog.name}, Breed: ${dog.breed}, Age: ${dog.age}, Weight: ${dog.weight}, Is available for adoption: ${dog.isAvailableForAdoption}, ShelterId: ${dog.shelterId}.`;
        dogListDiv.appendChild(dogItem);
    });
}

async function getDogById(id) {
    try {
        const response = await fetch(`/api/Dog/${id}`);
        if (!response.ok) {
            throw new Error('Dog not found');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching dog by ID:', error);
        return null; // Повертаємо null, якщо собака не знайдена
    }
}

// Функція для отримання собаки за іменем і породою з API
async function getDogByNameAndBreed(name, breed) {
    try {
        const response = await fetch(`/api/Dog/${name}/${breed}`);
        if (!response.ok) {
            throw new Error('Dog not found');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching dog by name and breed:', error);
        return null; // Повертаємо null, якщо собака не знайдена
    }
}

// Функція для відображення собаки за ID
async function displayDogById() {
    const id = document.getElementById('dog-id').value;
    if (!id) {
        alert("Please enter a dog ID.");
        return;
    }

    const dog = await getDogById(id);
    const dogDiv = document.getElementById('dog-by-id');
    dogDiv.innerHTML = ''; // Очищаємо перед відображенням нової інформації

    if (dog) {
        dogDiv.innerHTML = `
            <p>Name: ${dog.name}</p>
            <p>Breed: ${dog.breed}</p>
            <p>Age: ${dog.age}</p>
            <p>Weight: ${dog.weight}</p>
            <p>Available for Adoption: ${dog.isAvailableForAdoption}</p>
            <p>ShelterId: ${dog.shelterId}</p>
        `;
    } else {
        dogDiv.textContent = 'Dog not found or an error occurred.';
    }
}

// Функція для відображення собаки за іменем та породою
async function displayDogByNameAndBreed() {
    const name = document.getElementById('dog-name').value;
    const breed = document.getElementById('dog-breed').value;

    if (!name || !breed) {
        alert("Please enter both name and breed.");
        return;
    }

    const dog = await getDogByNameAndBreed(name, breed);
    const dogDiv = document.getElementById('dog-by-name-and-breed');
    dogDiv.innerHTML = ''; // Очищаємо перед відображенням нової інформації

    if (dog) {
        dogDiv.innerHTML = `
            <p>Name: ${dog.name}</p>
            <p>Breed: ${dog.breed}</p>
            <p>Age: ${dog.age}</p>
            <p>Weight: ${dog.weight}</p>
            <p>Available for Adoption: ${dog.isAvailableForAdoption}</p>
            <p>ShelterId: ${dog.shelterId}</p>
        `;
    } else {
        dogDiv.textContent = 'Dog not found or an error occurred.';
    }
}