/**
 * Định dạng date để hiển thị trên table
 * Author: TUANTA (13/08/2022)
 */
 export function convertDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [day, month, year].join('/');
}

/**
 * Định dạng date để binding vào form chi tiết
 * Author: TUANTA (13/08/2022)
 */
export function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

/**
 * Chuyển đổi định dạng tiền tệ 
 * Author: TUANTA (13/08/2022)
 */
export function formatCash(val){
    if(val){
        const str = val.toString()
        return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }
    else{
        return 0;
    }
}