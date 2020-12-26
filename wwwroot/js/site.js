window.onload = () => {
    const bad = document.getElementById("container-bad");
    const soso = document.getElementById("container-soso");
    const good = document.getElementById("container-good");

    let clicked = false;

    bad.addEventListener("click", e => {
        if(!clicked){
            clicked = true;
            fadeOut(soso);
            fadeOut(good);
            setToLeft(bad, -640);
            const text = addText("Thanks for voting!", "I'll try to make better codes!", bad);
            fadeIn(text);
            const face = document.getElementById("red-face");
            text.addEventListener("mouseenter", () => {face.classList.add("hover");});
            text.addEventListener("mouseleave", () => {face.classList.remove("hover");});
            sendRating("bad");
        }
    });

    soso.addEventListener("click", e => {
        if(!clicked){
            clicked = true;
            fadeOut(bad);
            fadeOut(good);
            setToLeft(soso, -320);
            const text = addText("Thanks for voting!", "I expect the next time you will be glanced", soso);
            fadeIn(text);
            const face = document.getElementById("yellow-face");
            text.addEventListener("mouseenter", () => {face.classList.add("hover");});
            text.addEventListener("mouseleave", () => {face.classList.remove("hover");});
            sendRating("soso");
        }
    });

    good.addEventListener("click", e => {
        if(!clicked){
            clicked = true;
            fadeOut(soso);
            fadeOut(bad);
            const text = addText("Thanks for voting!", "I am really happy you enjoyed my developments!", good);
            fadeIn(text);
            const face = document.getElementById("green-face");
            text.addEventListener("mouseenter", () => {face.classList.add("hover");});
            text.addEventListener("mouseleave", () => {face.classList.remove("hover");});
            sendRating("good");
        }
    });
}

async function fadeOut(element){
    let stopId;
    let progress = 1;
    function step(timestamp){
        if(progress <= 0){
            cancelAnimationFrame(stopId);
            return;
        }else{
            progress -= 0.05;
            element.style.opacity = progress;
            stopId = window.requestAnimationFrame(step);
        }
    }
    window.requestAnimationFrame(step);
}

async function setToLeft(element, until){
    let stopId;
    let progress = 0;
    function step(timestamp){
        if(progress <= until){
            cancelAnimationFrame(stopId);
            return;
        }else{
            progress -= 5;
            element.style.left = (progress + "px");
            stopId = window.requestAnimationFrame(step);
        }
    }
    window.requestAnimationFrame(step);
}

function addText(title, paragraph, container){
    container.innerHTML +=`
        <div style="display: none;" id="text-container">
            <h1>${title}</h1>
            <p>${paragraph}</p>
        </div>
    `;
    const text = document.getElementById("text-container");
    return text;
}

async function fadeIn(element){
    let stopId;
    let progress = 0;
    element.style.opacity = progress;
    element.style.display = "block";
    function step(timestamp){
        if(progress >= 1){
            cancelAnimationFrame(stopId);
            return;
        }else{
            progress += 0.05;
            element.style.opacity = progress;
            stopId = window.requestAnimationFrame(step);
        }
    }
    window.requestAnimationFrame(step);
}

function sendRating(review){
    let URL = window.location.href;
    URL = URL.split(window.location.pathname)[0];
    URL = `${URL}/Home/AddReview?review=${review}`;
    fetch(URL).then(res => res.json())
    .then(res => {
        if(res.ok){
            console.log("Llego", res.proms);
        }else{
            console.log("Error");
        }
    })
}