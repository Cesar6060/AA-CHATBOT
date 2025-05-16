window.initializeCalendar = (calendarElementId, events) => {
    var calendarEl = document.getElementById(calendarElementId);

    if (calendarEl) {
        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'timeGridWeek',
            headerToolbar: false, 
            height: 'auto',
            contentHeight: 250,
            slotMinTime: '08:00:00', 
            slotMaxTime: '18:00:00',
            slotDuration: '01:00:00', 
            slotLabelInterval: '02:00', 
            events: events
        });
        calendar.render();
    }
};

window.getEventsForToday = function(events) {
    const today = new Date().toISOString().split('T')[0]; 
    return events.filter(event => {
        const eventStartDate = new Date(event.Start); 
        const eventDate = eventStartDate.toISOString().split('T')[0];
        return eventDate === today;
    });
};