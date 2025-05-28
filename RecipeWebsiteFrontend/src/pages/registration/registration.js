import { SUPABASE_URL, SUPABASE_KEY } from '/src/shared/js/config.js';

const { createClient } = supabase;
const supabaseClient = createClient(SUPABASE_URL, SUPABASE_KEY);

document.getElementById('registration-form').addEventListener('submit', async (e) => {
    e.preventDefault(); // страница не перезагрузится 

    const email = document.getElementById('email').value
    const password = document.getElementById('password').value
    const confirmPassword = document.getElementById('confirm_password').value
    const name = document.getElementById('name').value
    const username = document.getElementById('username').value

    const data = { email: email, password: password, confirmPassword: confirmPassword, name: name, username: username }


    
    let response = await fetch('http://localhost:5232/api/Authentication/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })

    console.log(await response.json());
});