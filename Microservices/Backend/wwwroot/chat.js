document.addEventListener("DOMContentLoaded", () => {
    // <snippet_Connection>
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub", { accessTokenFactory: () => this.loginToken })
        .configureLogging(signalR.LogLevel.Information)
        .build();
        
    // </snippet_Connection>

    // <snippet_ReceiveMessage>
    connection.on("ReceiveMessage", (user, message) => {
        const li = document.createElement("li");
        li.textContent = `${user}: ${message}`;
        document.getElementById("messageList").appendChild(li);
    });
    // </snippet_ReceiveMessage>

    let jwt = "";
    let currentUser = "";

    let chatId = 0;

    document.getElementById("send").addEventListener("click", async () => {
        const message = document.getElementById("messageInput").value;
        chatId = document.getElementById("chatInput").value;
        
        // <snippet_Invoke>
        try {
            await connection.invoke("SendMessage", currentUser, message, jwt, chatId);
        } catch (err) {
            console.error(err);
        }
        // </snippet_Invoke>
    });

    document.getElementById("login").addEventListener("click", async () => {
        const user = document.getElementById("userInput").value;
        const password = document.getElementById("passwordInput").value;

        const url = "http://localhost:7135/Backend/login";

        let data = {
            "userName": user,
            "password": password
        };

        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        }).then((res) => {
            return res.json();
        }).then((data) => {
            if (data.accessToken.length > 0) {
                jwt = data.accessToken;
                currentUser = data.userName;
                console.log(data);
                document.getElementById("current-user").textContent = "User: " + currentUser;
                document.getElementsByClassName("login")[0].style.display = "none";
                document.getElementsByClassName("send-message")[0].style.display = "block";
            }
            else alert("Error login");
        });
    });

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    connection.onclose(async () => {
        await start();
    });

    // Start the connection.
    start();
});
