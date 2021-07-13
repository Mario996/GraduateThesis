import { v4 as uuidv4 } from 'uuid'

async function getAllPriorities () {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch('https://localhost:44332/priorities', requestOptions)
        .then(response => response.json())
}

async function getPriorityById (id) {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/priorities/${id}`, requestOptions)
        .then(response => response.json())
}

async function createPriority (priority) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: uuidv4(),
            Name: priority.name,
            Value: priority.value,
        }),
    }

    return fetch('https://localhost:44332/priorities', requestOptions)
        .then(response => response.json())
}

async function updatePriority (priority, id) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: id,
            Name: priority.name,
            Value: priority.value,
        }),
    }
    return fetch(`https://localhost:44332/priorities/${id}`, requestOptions)
        .then(response => response.json())
}

async function deletePriority (id) {
    const requestOptions = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/priorities/${id}`, requestOptions)
        .then(response => response.json())
}

export const prioritiesService = {
    createPriority,
    getAllPriorities,
    getPriorityById,
    deletePriority,
    updatePriority,
}
