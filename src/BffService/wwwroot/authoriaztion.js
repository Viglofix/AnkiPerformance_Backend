
let userClaims = null;
let claimFlag = false;

const claims_config = {
  headers: {
    "x-csrf": "1"
  },
  credentials: "include",
  method: "GET"
};

(async function () {
  var req = new Request("/bff/user", claims_config);

  try {
    let resp = await fetch(req);
    if (resp.ok) {
      userClaims = await resp.json();
      console.log(userClaims);
      document.querySelector("#results").innerHTML = "you're logged in";
      document.querySelector("#home").addEventListener("click",()=>{
        window.location = "http://localhost:5173"
      });
    } else if (resp.status === 401) {
      console.log("user not logged in");
      document.querySelector("#results").innerHTML = "you're not logged in";
    }
  } catch (e) {
    console.log("error checking user status");
  }
})();


/* function login() {
  window.location = "/bff/login";
}

function logout() {
  if (userClaims) {
    var logoutUrl = userClaims.find(
      (claim) => claim.type === "bff:logout_url"
    ).value;
    window.location = logoutUrl;
  } else {
    window.location = "/bff/logout";
  }
}

async function localApi() {
  var req = new Request("/local/identity", {
    headers: new Headers({
      "X-CSRF": "1",
    }),
  });

  try {
    var resp = await fetch(req);

    let data;
    if (resp.ok) {
      data = await resp.json();
    }
    console.log("Local API Result: " + resp.status, data);
  } catch (e) {
    console.log("error calling local API");
  }
}

async function remoteApi() {
  var req = new Request("/remote/identity", {
    headers: new Headers({
      "X-CSRF": "1",
    }),
  });

  try {
    var resp = await fetch(req);

    let data;
    if (resp.ok) {
      data = await resp.json();
    }
    console.log("Remote API Result: " + resp.status, data);
  } catch (e) {
    console.log("error calling remote API");
  }
} */