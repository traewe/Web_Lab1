async function getShelters() {
    try {
        const response = await fetch('/api/DogShelter');
        if (!response.ok) {
            throw new Error('Failed to fetch shelters');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching shelters:', error);
        return []; 
    }
}

async function displayShelters() {
    const shelters = await getShelters();
    const shelterListDiv = document.getElementById("shelter-list");
    shelterListDiv.innerHTML = '';

    if (shelters.length === 0) {
        shelterListDiv.textContent = 'No shelters available or an error occurred.';
        return;
    }

    shelters.forEach(shelter => {
        const shelterItem = document.createElement("div");
        shelterItem.innerHTML = `
            <h3>Shelter: ${shelter.name}</h3>
            <p>Id: ${shelter.id}</p>
            <p>Address: ${shelter.address}</p>
            <p>Contact Number: ${shelter.contactNumber}</p>
        `;

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
        return null;
    }
}

async function getShelterByNameAndAddress(name, address) {
    try {
        const response = await fetch(`/api/DogShelter/${name}/${address}`);
        if (!response.ok) {
            throw new Error('Shelter not found');
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching shelter by name and address:', error);
        return null;
    }
}

async function displayShelterById() {
    const id = document.getElementById('shelter-id').value;
    if (!id) {
        alert("Please enter a shelter ID.");
        return;
    }

    const shelter = await getShelterById(id);
    const shelterDiv = document.getElementById('shelter-by-id');
    shelterDiv.innerHTML = '';

    if (shelter) {
        shelterDiv.innerHTML = `
            <p>Id: ${shelter.id}</p>
            <p>Shelter: ${shelter.name}</p>
            <p>Address: ${shelter.address}</p>
            <p>Contact Number: ${shelter.contactNumber}</p>
        `;

        if (shelter.dogs && shelter.dogs.length > 0) {
            const dogList = document.createElement('ul');
            shelter.dogs.forEach(dog => {
                const dogItem = document.createElement('li');
                dogItem.textContent = `Name: ${dog.name}, Breed: ${dog.breed}, Age: ${dog.age}, Weight: ${dog.weight}`;
                dogList.appendChild(dogItem);
            });
            shelterDiv.appendChild(dogList);
        } else {
            shelterDiv.innerHTML += `<p>No dogs found in this shelter.</p>`;
        }
    } else {
        shelterDiv.textContent = 'Shelter not found or an error occurred.';
    }
}

async function displayShelterByNameAndAddress() {
    const name = document.getElementById('shelter-name-search').value;
    const address = document.getElementById('shelter-address-search').value;

    if (!name || !address) {
        alert("Please enter both name and address.");
        return;
    }

    const shelter = await getShelterByNameAndAddress(name, address);
    const shelterDiv = document.getElementById('shelter-by-name-and-address');
    shelterDiv.innerHTML = '';

    if (shelter) {
        shelterDiv.innerHTML = `
            <p>Id: ${shelter.id}</p>
            <p>Shelter: ${shelter.name}</p>
            <p>Address: ${shelter.address}</p>
            <p>Contact Number: ${shelter.contactNumber}</p>
        `;

        if (shelter.dogs && shelter.dogs.length > 0) {
            const dogList = document.createElement('ul');
            shelter.dogs.forEach(dog => {
                const dogItem = document.createElement('li');
                dogItem.textContent = `Name: ${dog.name}, Breed: ${dog.breed}, Age: ${dog.age}, Weight: ${dog.weight}`;
                dogList.appendChild(dogItem);
            });
            shelterDiv.appendChild(dogList);
        } else {
            shelterDiv.innerHTML += `<p>No dogs found in this shelter.</p>`;
        }
    } else {
        shelterDiv.textContent = 'Shelter not found or an error occurred.';
    }
}


async function addShelter(event) {
    event.preventDefault();

    const name = document.getElementById('shelter-name').value;
    const address = document.getElementById('shelter-address').value;
    const contactNumber = document.getElementById('shelter-contact').value;

    if (!name || !address || !contactNumber) {
        alert("Please fill in all required fields.");
        return;
    }

    const shelterData = {
        name: name,
        address: address,
        contactNumber: contactNumber
    };

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