const formatPrice = (price) => new Intl.NumberFormat('Vi-VN', {style: 'currency', currency: 'VND'}).format(price)
const formatNumber = (number, decimalPlaces) => {
    return number.toFixed(decimalPlaces);
};

export { formatPrice, formatNumber }