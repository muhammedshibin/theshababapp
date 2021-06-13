export function getMonthArray(){
    const monthNames = [
        {value:1,display:'January'},
        {value:2,display:'February'},
        {value:3,display:'March'},
        {value:4,display:'April'},
        {value:5,display:'May'},
        {value:6,display:'June'},
        {value:7,display:'July'},
        {value:8,display:'August'},
        {value:9,display:'September'},
        {value:10,display:'October'},
        {value:11,display:'November'},
        {value:12,display:'December'}
      ];
    return monthNames;
}

export function getYears(){
    let years = [];
    const currentYear = new Date().getFullYear();
    const previousYear = currentYear-1;
    const yearBeforeThat = previousYear-1;
    const nextYear = currentYear +1;
    const yearAfterThat = nextYear+1;
    years.push(yearBeforeThat,previousYear,currentYear,nextYear,yearAfterThat);
    return years;
}