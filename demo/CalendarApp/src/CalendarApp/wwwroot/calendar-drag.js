export function enableDragDrop(elem, componentInstance) {
    new Draggable.Droppable(elem, {
        draggable: '.dragme',
        dropzone: '.slot',
        delay: 0,
        distance: 10,
        mirror: { constrainDimensions: true }
    }).on('droppable:dropped', evt => {
        // Only allow dropping into empty slots
        if (evt.dropzone.childElementCount > 0) {
            evt.cancel();
        }
    }).on('droppable:stop', async evt => {
        if (componentInstance) {
            // We have a component instance. Call a method on it.
            const bookingId = parseInt(evt.dragEvent.source.dataset.bookingId);
            const toDateTime = evt.dropzone.dataset.slotDatetime;
            const toRoomId = parseInt(evt.dropzone.dataset.slotRoomId);
            await componentInstance.invokeMethodAsync(
                'HandleMove', bookingId, toRoomId, toDateTime);
        } else {
            // There's no component instance. This is an error.
            throw new Error('Cannot notify component, because no component was supplied');
        }
    });
}
