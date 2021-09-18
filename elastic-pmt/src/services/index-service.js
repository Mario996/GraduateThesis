async function create () {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
    }

    return fetch('https://localhost:44332/index', requestOptions)
        .then((response) => response.json())
}

export const indexService = {
    create,
}
