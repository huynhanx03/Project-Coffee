import { useNavigation } from "@react-navigation/native";
import React, { useEffect, useRef, useState } from "react";
import {
    Image,
    Platform,
    ScrollView,
    Text,
    TextInput,
    TouchableOpacity,
    View,
} from "react-native";
import * as Icons from "react-native-heroicons/solid";
import { Divider } from "react-native-paper";
import { useDispatch } from "react-redux";
import { setCart } from "../controller/CartController";
import { addToCart } from "../redux/slices/cartSlice";
import { colors } from "../theme";
import { formatPrice } from "../utils";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import ItemReviewList from "../components/itemReviewList";
import { Rating } from "react-native-ratings";
import { getReview } from "../controller/ReviewController";
import ShowToast from "../components/toast";
import Animated from "react-native-reanimated";
const ios = Platform.OS === "ios";

const DetailItemScreen = ({ route }) => {
    const navigation = useNavigation();
    const dispatch = useDispatch();
    const product = route.params;

    const initialPrice = product.initialPrice;
    const initialSize = product.initialSize;

    const [size, setSize] = useState(initialSize);
    const [sizeString, setSizeString] = useState(
        initialSize === "S" ? "Nho" : size === "M" ? "Thuong" : "Lon"
    );
    const [quantity, setQuantity] = useState(1);
    const [price, setPrice] = useState(initialPrice);
    const [priceOrigin, setPriceOrigin] = useState(0);
    const [total, setTotal] = useState(initialPrice);
    const [reviewList, setReviewList] = useState([]);
    const [ratingPoint, setRatingPoint] = useState(0);
    const scrollRef = useRef(null);

    const handleSizeAndPrice = (sizeProp) => {
        const string =
            sizeProp === "S" ? "Nho" : sizeProp === "M" ? "Thuong" : "Lon";

        setSize(sizeProp);
        setSizeString(string);
        setPrice(product.Size[string].Gia);
        setPriceOrigin(product.Size[string].Gia);

        //Discount
        if (product.PhanTramGiam) {
            const discount = product?.PhanTramGiam / 100;
            setPrice(product.Size[string].Gia * (1 - discount));
        }
    };

    useEffect(() => {
        const string =
            initialSize === "S"
                ? "Nho"
                : initialSize === "M"
                ? "Thuong"
                : "Lon";
        setPriceOrigin(product.Size[string].Gia);
    }, []);

    const handleTotal = () => {
        setTotal(price * quantity);
    };

    useEffect(() => {
        handleTotal();
    }, [price, quantity]);

    const handleIncreaseQuantity = () => {
        if (quantity < product.SoLuong) {
            setQuantity((quantity) => quantity + 1);
        } else {
            setQuantity(product.SoLuong);
            ShowToast("error", "Lỗi", "Số lượng sản phẩm không đủ");
        }
    };

    const handleQuantityInput = () => {
        if (quantity > product.SoLuong) {
            setQuantity(product.SoLuong);
            ShowToast("error", "Lỗi", "Số lượng sản phẩm không đủ");
            return;
        }

        if (quantity < 1) {
            setQuantity(1);
            ShowToast("error", "Lỗi", "Số lượng sản phẩm phải lớn hơn 0");
            return;
        }
    };

    const handleDecreaseQuantity = () => {
        if (quantity > 1) {
            setQuantity((quantity) => quantity - 1);
        } else {
            ShowToast("error", "Lỗi", "Số lượng sản phẩm phải lớn hơn 0");
        }
    };

    const handleAddCart = (item) => {
        const itemCart = {
            TenSanPham: item.TenSanPham,
            Gia: price,
            GiaGoc: priceOrigin,
            HinhAnh: item.HinhAnh,
            KichThuoc: size,
            MaSanPham: item.MaSanPham,
            SoLuong: item.quantity,
            PhanTramGiam: item.PhanTramGiam,
        };
        dispatch(addToCart(itemCart));
        setCart(itemCart);
        ShowToast("success", "Thông báo", "Thêm vào giỏ hàng thành công");
    };

    const handleBuyNow = (item) => {
        const itemCart = {
            TenSanPham: item.TenSanPham,
            Gia: price,
            GiaGoc: priceOrigin,
            HinhAnh: item.HinhAnh,
            KichThuoc: size,
            MaSanPham: item.MaSanPham,
            SoLuong: item.quantity,
            PhanTramGiam: item.PhanTramGiam,
        };
        navigation.navigate("Prepare", { ...itemCart });
    };

    const handleGetReview = async () => {
        const reviews = await getReview(product.MaSanPham);
        if (reviews && reviews.length > 0) {
            let totalRatingPoint = 0;
            reviews.forEach((review) => {
                totalRatingPoint += review?.DiemDanhGia;
            });
            let point = totalRatingPoint / reviews.length;
            setRatingPoint(point);
            setReviewList(reviews);
        }
    };

    useEffect(() => {
        handleGetReview();
    }, []);
    return (
        <View className="flex-1">
            <TouchableOpacity
                onPress={() => navigation.goBack()}
                className="absolute top-10 z-10 left-5 rounded-full p-3 bg-amber-400"
            >
                <View>
                    <Icons.ChevronLeftIcon size={30} color={"black"} />
                </View>
            </TouchableOpacity>
            <ScrollView
                ref={scrollRef}
                className="flex-[6]"
                showsVerticalScrollIndicator={false}
            >
                {/* image */}
                <Image
                    source={{ uri: product.HinhAnh }}
                    resizeMode="cover"
                    style={{
                        width: "100%",
                        height: hp(50),
                        width: wp(100),
                        borderRadius: 16,
                    }}
                />

                {/* info */}
                <View
                    style={{
                        marginTop: wp(-12),
                        backgroundColor: "#f2f2f2",
                    }}
                    className="rounded-3xl pt-3 space-y-2"
                >
                    <View className="flex-row mx-5 justify-between">
                        <Text
                            style={{ color: colors.text(1) }}
                            className="font-semibold text-2xl"
                        >
                            {product.TenSanPham}
                        </Text>
                        <View className="flex-row items-end space-x-3">
                            {product.PhanTramGiam && (
                                <Text className="line-through text-red-500 font-semibold text-lg">
                                    {formatPrice(product.Size[sizeString].Gia)}
                                </Text>
                            )}

                            <Text
                                style={{ color: colors.text(1) }}
                                className="font-semibold text-2xl"
                            >
                                {formatPrice(price)}
                            </Text>
                        </View>
                    </View>
                    {/* description */}
                    <View className="mx-5">
                        <Text numberOfLines={2} className="text-base">
                            {product.Mota}
                        </Text>
                    </View>
                    {/* star */}
                    <View className="flex-row mx-5 items-center space-x-3">
                        <Icons.StarIcon size={24} color={"#fbbe21"} />
                        <Text className="text-base font-semibold">
                            {ratingPoint}/5
                        </Text>
                    </View>
                    <Divider />
                    {/* size */}
                    <View className="space-y-1 mx-5">
                        <Text
                            className="text-base font-semibold"
                            style={{ color: colors.text(1) }}
                        >
                            Kích cỡ
                        </Text>
                        <View className="flex-row justify-between">
                            <TouchableOpacity
                                onPress={() => handleSizeAndPrice("S")}
                                className="rounded-xl border"
                                style={{
                                    borderColor:
                                        size === "S"
                                            ? colors.active
                                            : "#dedede",
                                    backgroundColor:
                                        size === "S" ? "#fff5ee" : "#f2f2f2",
                                }}
                            >
                                <Text
                                    style={{
                                        paddingHorizontal: wp(13),
                                        paddingVertical: wp(5),
                                    }}
                                >
                                    S
                                </Text>
                            </TouchableOpacity>
                            <TouchableOpacity
                                onPress={() => handleSizeAndPrice("M")}
                                className="rounded-xl border"
                                style={{
                                    borderColor:
                                        size === "M"
                                            ? colors.active
                                            : "#dedede",
                                    backgroundColor:
                                        size === "M" ? "#fff5ee" : "#f2f2f2",
                                }}
                            >
                                <Text
                                    style={{
                                        paddingHorizontal: wp(13),
                                        paddingVertical: wp(5),
                                    }}
                                >
                                    M
                                </Text>
                            </TouchableOpacity>
                            <TouchableOpacity
                                onPress={() => handleSizeAndPrice("L")}
                                className="rounded-xl border"
                                style={{
                                    borderColor:
                                        size === "L"
                                            ? colors.active
                                            : "#dedede",
                                    backgroundColor:
                                        size === "L" ? "#fff5ee" : "#f2f2f2",
                                }}
                            >
                                <Text
                                    style={{
                                        paddingHorizontal: wp(13),
                                        paddingVertical: wp(5),
                                    }}
                                >
                                    L
                                </Text>
                            </TouchableOpacity>
                        </View>
                    </View>
                    {/* quantity */}
                    <View className="space-y-1 mx-5 mb-5">
                        <Text
                            className="text-base font-semibold"
                            style={{ color: colors.text(1) }}
                        >
                            Số lượng
                        </Text>
                        <View className="flex-row justify-center space-x-4 items-center">
                            <TouchableOpacity
                                onPress={handleDecreaseQuantity}
                                className="rounded-md p-3"
                                style={{ backgroundColor: colors.primary }}
                            >
                                <Icons.MinusIcon
                                    size={15}
                                    color="white"
                                    strokeWidth={4}
                                />
                            </TouchableOpacity>
                            <View className="bg-white border border-neutral-400 rounded-md">
                                {/* <Text className="text-base font-semibold">
                                        {quantity}
                                    </Text> */}
                                <TextInput
                                    className="p-3 px-4"
                                    value={quantity.toString()}
                                    onBlur={handleQuantityInput}
                                    onChangeText={(e) => setQuantity(+e)}
                                />
                            </View>
                            <TouchableOpacity
                                onPress={handleIncreaseQuantity}
                                className="rounded-md p-3"
                                style={{ backgroundColor: colors.primary }}
                            >
                                <Icons.PlusIcon
                                    size={15}
                                    color="white"
                                    strokeWidth={4}
                                />
                            </TouchableOpacity>
                        </View>
                    </View>

                    {/* review */}
                    <View className="mx-5">
                        <Text className="text-lg font-bold">
                            Đánh giá sản phẩm
                        </Text>
                        <View className="flex-row items-center">
                            <Rating
                                tintColor="#f2f2f2"
                                readonly
                                startingValue={ratingPoint}
                                type="star"
                                style={{
                                    alignItems: "flex-start",
                                    marginRight: 10,
                                }}
                            />
                            <Text className="text-base font-semibold mr-1">
                                {ratingPoint}/5
                            </Text>
                            <Text className="text-base text-gray-400">
                                ({reviewList.length} đánh giá)
                            </Text>
                        </View>
                    </View>
                    <View className="mt-10 mx-5">
                        <View>
                            {reviewList && (
                                <ItemReviewList reviewList={reviewList} />
                            )}
                        </View>
                    </View>
                </View>
            </ScrollView>
            {/* add to cart */}
            <View className="bg-white flex-1 rounded-lg justify-center shadow-md">
                <View className="mx-5 flex-row justify-between items-center">
                    <View>
                        <Text className="text-base font-semibold text-gray-700">
                            Tổng
                        </Text>
                        <Text className="text-xl font-semibold">
                            {formatPrice(total)}
                        </Text>
                    </View>
                    <View className="flex-row space-x-2">
                        <TouchableOpacity
                            onPress={() =>
                                handleBuyNow({ ...product, quantity })
                            }
                            className="flex-row bg-red-500 rounded-lg p-3 px-6 items-center justify-center"
                        >
                            <Text className="text-lg font-semibold">
                                Mua ngay
                            </Text>
                        </TouchableOpacity>
                        <TouchableOpacity
                            onPress={() =>
                                handleAddCart({ ...product, quantity })
                            }
                            className="flex-row rounded-lg p-3 px-5 items-center justify-center"
                            style={{ backgroundColor: colors.active }}
                        >
                            <Icons.ShoppingCartIcon
                                size={30}
                                color={colors.primary}
                            />
                        </TouchableOpacity>
                    </View>
                </View>
            </View>
        </View>
    );
};

export default DetailItemScreen;
