const clock = document.querySelector(".clock")
let days = ["Dimanche", "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi"]
let months = ["Janvier", "Fevrier", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Décembre"]

function zeros(num) {
    if (num < 10) {
        num = '0' + num
    };
    return num;
}

function dateClock() {
    let date = new Date()
    let day = days[date.getDay()]
    let nbDay = date.getDate()
    let month = months[date.getMonth()]
    let year = date.getFullYear()
    let hour = date.getHours()
    let min = zeros(date.getMinutes())
    let sec = zeros(date.getSeconds())
    let displayClock = `${day} ${nbDay} ${month} ${year} ${hour}:${min}:${sec}`
    clock.textContent = displayClock
}

setInterval(dateClock,500)
dateClock()

function myFunction() {
    var element = document.body
    element.classList.toggle("dark-mode")
}

const btn = document.querySelector(".btn-toggle")
const prefersDarkScheme = window.matchMedia("(prefers-color-scheme: dark)")

const currentTheme = localStorage.getItem("theme")
if (currentTheme == "dark") {
    document.body.classList.toggle("dark-theme")
    
} else if (currentTheme == "light") {
    document.body.classList.toggle("light-theme")
}


    if (document.body.classList.contains("light-theme")) {
        btn.textContent = "Mode sombre"
    }
    else {
        btn.textContent = "Mode clair"
    }



btn.addEventListener("click", function () {
    if (prefersDarkScheme.matches) {
        document.body.classList.toggle("light-theme")
        var theme = document.body.classList.contains("light-theme")
            ? "light"
            : "dark"
    } else {
        document.body.classList.toggle("dark-theme")
        var theme = document.body.classList.contains("dark-theme")
            ? "dark"
            : "light"
    }
    if (theme == "dark") {
        btn.textContent = "Mode clair"
    }
    else if (theme == "light") {
        btn.textContent = "Mode sombre"
      }
    localStorage.setItem("theme", theme)
})

const btnSearch = document.querySelector(".search")
const linkList = document.querySelector(".linkList")
let check = true

console.log(linkList)
console.log(btnSearch)

btnSearch.addEventListener("click", function () {

    if (check) {
        //create new a
        let elem = document.createElement('a')
        // add href
        elem.href = "#"
        // create text node
        let linkLabel = document.createTextNode("Retour à la liste")
        //add texte in element a
        elem.appendChild(linkLabel)
        //select the element where we want to add the link
        document.querySelector(".linkList").appendChild(elem)
        check = false
        }

})