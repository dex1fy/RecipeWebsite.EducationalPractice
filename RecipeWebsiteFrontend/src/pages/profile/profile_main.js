// async function test1(){
//     document.write("hello")
// }
// test1()

async function UsersData() {
    let token = localStorage.getItem("access_token");

    try {
        let response = await fetch("http://localhost:5232/api/Profile/Profile", {
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
        let userName2 = document.getElementById("userName2");
        let userNickName2 = document.getElementById("userNickName2");
        userName2.innerHTML = data.name;
        userNickName2.innerHTML = data.userName;
        
    } catch (err) {
        console.error("Ошибка при получении пользователей:", err);
    }
}

// async function UsersUpdate(){

//     let token = localStorage.getItem("access_token");

//     try {
//         let response = await fetch("http://localhost:5232/api/Profile/Profile", {
//             headers: {
//                 "Content-Type": "application/json",
//                 Authorization: "Bearer " + token,
                
//             },
//         });

//         if (!response.ok) {
//             throw new Error(`Ошибка: ${response.status}`);
//         }

//         let data = await response.json();





//     } catch (err) {
//         console.error("Ошибка при получении пользователей:", err);
//     }
// }

UsersData();