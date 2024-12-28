async function getDogs() {
    try {
        const response = await fetch('/api/Dog');
        if (!response.ok) {
            throw new Error('Failed to fetch dogs');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching dogs:', error);
        return [];
    }
}

async function displayDogs() {
    const dogs = await getDogs();
    const dogListDiv = document.getElementById("dog-list");
    dogListDiv.innerHTML = '';

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
        return null;
    }
}

async function getDogByNameAndBreed(name, breed) {
    try {
        const response = await fetch(`/api/Dog/${name}/${breed}`);
        if (!response.ok) {
            throw new Error('Dog not found');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching dog by name and breed:', error);
        return null;
    }
}

async function displayDogById() {
    const id = document.getElementById('dog-id').value;
    if (!id) {
        alert("Please enter a dog ID.");
        return;
    }

    const dog = await getDogById(id);
    const dogDiv = document.getElementById('dog-by-id');
    dogDiv.innerHTML = '';

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

async function displayDogByNameAndBreed() {
    const name = document.getElementById('dog-name-search').value;
    const breed = document.getElementById('dog-breed-search').value;

    if (!name || !breed) {
        alert("Please enter both name and breed.");
        return;
    }

    const dog = await getDogByNameAndBreed(name, breed);
    const dogDiv = document.getElementById('dog-by-name-and-breed');
    dogDiv.innerHTML = '';

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

async function addDog(event) {
    event.preventDefault();

    const name = document.getElementById('dog-name').value;
    const breed = document.getElementById('dog-breed').value;
    const age = document.getElementById('dog-age').value;
    const weight = document.getElementById('dog-weight').value;
    const isAvailableForAdoption = document.getElementById('dog-isAvailableForAdoption').checked;
    const shelterId = document.getElementById('dog-shelterId').value || null;

    if (!name || !breed || !age || !weight) {
        alert("Please fill in all required fields.");
        return;
    }
    if (!name || !breed || !age || !weight) {
        alert("Please fill in all required fields.");
        return;
    }
    if (!name || !breed || !age || !weight) {
        alert("Please fill in all required fields.");
        return;
    }
    if (!name || !breed || !age || !weight) {
        alert("Please fill in all required fields.");
        return;
    }

    if (isNaN(age) || isNaN(weight)) {
        alert("Age and weight must be valid numbers.");
        return;
    }

    if (age <= 0 || weight <= 0) {
        alert("Age and weight must be positive values.");
        return;
    }

    const dogData = {
        name: name,
        breed: breed,
        age: parseInt(age),
        weight: parseFloat(weight),
        isAvailableForAdoption: isAvailableForAdoption,
        shelterId: shelterId ? parseInt(shelterId) : null
    };

    try {
        const response = await fetch('/api/Dog', {
            method: 'POST',
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            },
            body: JSON.stringify(dogData)
        })
            .then((response) => response.json())
            .then((json) => console.log(json));
    } catch (error) {
        console.error('Error adding dog:', error);
    }
}

async function deleteDog() {
    const dogId = document.getElementById('delete-dog-id').value;

    if (!dogId) {
        alert("Please enter a dog ID.");
        return;
    }

    try {
        const response = await fetch(`/api/Dog/${dogId}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            document.getElementById('delete-dog-response').textContent = `Dog with ID ${dogId} deleted successfully.`;
        } else {
            const result = await response.json();
            document.getElementById('delete-dog-response').textContent = `Error: ${result.message || 'Failed to delete dog.'}`;
        }
    } catch (error) {
        console.error('Error deleting dog:', error);
        document.getElementById('delete-dog-response').textContent = 'Error deleting dog.';
    }
}

async function updateDog(event) {
    event.preventDefault();

    const dogId = document.getElementById('update-dog-id').value;
    const name = document.getElementById('update-dog-name').value;
    const breed = document.getElementById('update-dog-breed').value;
    const age = document.getElementById('update-dog-age').value;
    const weight = document.getElementById('update-dog-weight').value;
    const isAvailableForAdoption = document.getElementById('update-dog-available').checked;
    const shelterId = document.getElementById('update-dog-shelterId').value || null;

    const dogData = {
        Name: name,
        Breed: breed,
        Age: parseInt(age),
        Weight: parseFloat(weight),
        IsAvailableForAdoption: isAvailableForAdoption,
        ShelterId: shelterId ? parseInt(shelterId) : null
    };

    try {
        const response = await fetch(`/api/Dog/${dogId}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dogData)
        });
    } catch (error) {
        console.error('Error updating dog:', error);
    }
}
