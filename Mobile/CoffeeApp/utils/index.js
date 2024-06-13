const formatPrice = (price) => new Intl.NumberFormat('Vi-VN', {style: 'currency', currency: 'VND'}).format(price)
const formatNumber = (number, decimalPlaces) => {
    return number.toFixed(decimalPlaces);
};
const blurhash =
  '|rF?hV%2WCj[ayj[a|j[az_NaeWBj@ayfRayfQfQM{M|azj[azf6fQfQfQIpWXofj[ayj[j[fQayWCoeoeaya}j[ayfQa{oLj?j[WVj[ayayj[fQoff7azayj[ayj[j[ayofayayayj[fQj[ayayj[ayfjj[j[ayjuayj[';

export { formatPrice, formatNumber, blurhash }