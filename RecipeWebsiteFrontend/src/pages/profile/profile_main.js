// async function test1(){
//     document.write("hello")
// }
// test1()

async function UsersData() {
    let token = localStorage.getItem("access_token");

    try {
        let response = await fetch("http://localhost:5232/api/Profile", {
            headers: {
                "Content-Type": "application/json",
                Authorization: "Bearer " + token,
            },
        });

        if (!response.ok) {
            throw new Error(`Ошибка: ${response.status}`);
        }

        let data = await response.json();
        console.log(data);
        let userName2 = document.createElement("div");
        let userNickName2 = document.createElement("div");

        data.usersName = users.name; 
        data.usersNickName = users.username; 
        userName2.appendChild(data.usersName);
        userNickName2.appendChild(data.usersNickName);
        
    } catch (err) {
        console.error("Ошибка при получении пользователей:", err);
    }
}