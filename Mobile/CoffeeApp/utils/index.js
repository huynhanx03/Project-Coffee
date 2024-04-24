const formatPrice = (price) => new Intl.NumberFormat('Vi-VN', {style: 'currency', currency: 'VND'}).format(price)

export { formatPrice }