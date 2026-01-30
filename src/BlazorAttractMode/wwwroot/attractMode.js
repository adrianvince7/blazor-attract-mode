// Blazor Attract Mode - Lightweight interaction detector
let inactivityTimer;
let dotNetRef;
let timeoutMs;
let isAttractModeActive = false;
const events = ['mousedown', 'mousemove', 'keydown', 'touchstart', 'scroll', 'click'];

function resetTimer() {
    if (isAttractModeActive) {
        isAttractModeActive = false;
        dotNetRef.invokeMethodAsync('OnAttractModeChanged', false);
    }
    
    clearTimeout(inactivityTimer);
    inactivityTimer = setTimeout(() => {
        if (!isAttractModeActive) {
            isAttractModeActive = true;
            dotNetRef.invokeMethodAsync('OnAttractModeChanged', true);
        }
    }, timeoutMs);
}

function handleUserActivity() {
    resetTimer();
}

export function initialize(dotNetReference, timeout) {
    dotNetRef = dotNetReference;
    timeoutMs = timeout;
    
    // Add event listeners - use passive for scroll-related events only
    events.forEach(event => {
        const options = (event === 'scroll' || event === 'mousemove') 
            ? { passive: true } 
            : false;
        document.addEventListener(event, handleUserActivity, options);
    });
    
    // Start the timer
    resetTimer();
}

export function dispose() {
    clearTimeout(inactivityTimer);
    events.forEach(event => {
        document.removeEventListener(event, handleUserActivity);
    });
    dotNetRef = null;
}
