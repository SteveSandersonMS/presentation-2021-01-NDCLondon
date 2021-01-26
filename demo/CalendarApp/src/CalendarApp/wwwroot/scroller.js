export function scrollTo(elem, proportion) {
    document.scrollingElement.scrollTo(0, 
        elem.offsetTop - document.querySelector('nav').offsetHeight
        + elem.offsetHeight*proportion);
}
