var periodToPaymentDictionarry = new Object;
periodToPaymentDictionarry[1] = 2;
periodToPaymentDictionarry[2] = 300;
periodToPaymentDictionarry[3] = 500;

function changeVipPeriod(period) {
    var noneStyle = "border-bottom: none;border-top: none;border-right: none; border-left:none; background: #D8E6F3;";
    clearStyles(noneStyle);
    setVIPstatusType(period);
    var uid = uuidv4();
    console.log(uid);
    $('#transactionId').val(uid);

    var tr = $('.payment-price-table tbody tr')[period];
    var borderColor = "green";
    var borderBottom = `border-bottom: 2px solid ${borderColor}`;
    var borderTop = `border-top: 2px solid ${borderColor}`;
    var borderLeft = `border-left: 2px solid ${borderColor}`;
    var borderRight = `border-right: 2px solid ${borderColor}`;

    var background = "background: rgb(146, 197, 241);";

    var leftTd = `${borderBottom}; ${borderTop}; ${borderLeft}; ${background};`;
    var rightTd = `${borderBottom}; ${borderTop}; ${borderRight}; ${background};`;
    var centerTd = `${borderBottom}; ${borderTop}; ${background};`;

    tr.children[0].style = leftTd;
    tr.children[1].style = centerTd;
    tr.children[2].style = rightTd;
}


function clearStyles(noneStyle) {
    var tr = $('.payment-price-table tbody tr');
    var tds = tr.children('td');
    [].forEach.call(tds, function (td) {
        td.style = noneStyle;
    });
}

function setVIPstatusType(type) {
    $('#transactionAmount').val(periodToPaymentDictionarry[type]);
    $('#transactionVipType').val(type);
}

function startTransaction() {
    var request = new Object();
    request.TransactionId = $('#transactionId').val();
    request.Type = $('#transactionVipType').val();
    request.Amount = $('#transactionAmount').val();

    $.ajax({
        url: "/api/starttransaction",
        type: "post",
        data: request
    });
}

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
} 