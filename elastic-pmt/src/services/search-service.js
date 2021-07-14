async function search (query) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            query,
        }),
    }

    return fetch('https://localhost:44332/search', requestOptions)
        .then((response) => response.json())
}

async function create () {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
    }

    return fetch('https://localhost:44332/search/create-index', requestOptions)
        .then((response) => response.json())
}

export const searchService = {
    search,
    create
}
