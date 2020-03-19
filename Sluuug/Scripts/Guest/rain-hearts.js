const NUMBER_OF_LEAVES = 20;

function init()
{
    var container = document.getElementById('heartsContainer');
    for (var i = 0; i < NUMBER_OF_LEAVES; i++) 
    {
        container.appendChild(createALeaf());
    }
}

function randomInteger(low, high)
{
    return low + Math.floor(Math.random() * (high - low));
}

function randomFloat(low, high)
{
    return low + Math.random() * (high - low);
}


function pixelValue(value)
{
    return value + 'px';
}

function durationValue(value)
{
    return value + 's';
}

function createALeaf()
{
    var heartDiv = document.createElement('div');
    var image = document.createElement('img');
    
    image.src = '/Resources/images/rain-heart-1.svg';  //randomInteger(1, 5) + '.svg';
    image.style.zIndex = randomFloat(999, 1000);

    heartDiv.style.top = "-20px";
    heartDiv.style.zIndex = randomFloat(999, 1000);

    heartDiv.style.left = pixelValue(randomInteger(0, window.innerWidth - 100));
    
    var spinAnimationName = (Math.random() < 0.5) ? 'clockwiseSpin' : 'counterclockwiseSpinAndFlip';
    
    heartDiv.style.webkitAnimationName = 'fade, drop';
    image.style.webkitAnimationName = spinAnimationName;
    
    var fadeAndDropDuration = durationValue(randomFloat(5, 11));

    var spinDuration = durationValue(randomFloat(4, 8));
    heartDiv.style.webkitAnimationDuration = fadeAndDropDuration + ', ' + fadeAndDropDuration;

    var leafDelay = durationValue(randomFloat(0, 5));
    heartDiv.style.webkitAnimationDelay = leafDelay + ', ' + leafDelay;

    image.style.webkitAnimationDuration = spinDuration;
    heartDiv.appendChild(image);
    return heartDiv;
}

window.addEventListener('load', init, false);
