import { useNavigation } from "@react-navigation/native";
import React, { useEffect, useState } from "react";
import { Image, ScrollView, Text, TextInput, TouchableOpacity, View } from "react-native";
import Draggable from "react-native-draggable";
import * as Icons from "react-native-heroicons/outline";
import Carousel from "react-native-reanimated-carousel";
import { heightPercentageToDP as hp, widthPercentageToDP as wp } from "react-native-responsive-screen";
import Categories from "../components/categories";
import Item from "../components/item";
import getDefaultAddress from "../customHooks/getDefaultAddress";
import { colors } from "../theme";
import { getProducts } from "../controller/ProductController";

const HomeScreen = () => {
    const navigation = useNavigation();
    const [isActive, setIsActive] = useState("Tất cả");
    const [products, setProducts] = useState(null);
    const categories = ["Espresso", "Machiato", "Latte", "America"];
    const quantity = 2;

    const addressData = getDefaultAddress();

    const handleGetProducts = async () => {
        let allProducts = [];
        const listProducts = await getProducts();
        if (listProducts) {
            for (const key in listProducts) {
                const productData = {
                    MaSanPham: listProducts[key].MaSanPham,
                    TenSanPham: listProducts[key].TenSanPham,
                    HinhAnh: listProducts[key].HinhAnh,
                    Size: {
                        Nho: {
                            TenKichThuoc: listProducts[key]?.ChiTietKichThuocSanPham?.KT0001?.TenKichThuoc,
                            Gia: listProducts[key]?.ChiTietKichThuocSanPham?.KT0001?.Gia
                        },
                        Thuong: {
                            TenKichThuoc: listProducts[key]?.ChiTietKichThuocSanPham?.KT0002?.TenKichThuoc,
                            Gia: listProducts[key]?.ChiTietKichThuocSanPham?.KT0002?.Gia
                        },
                        Lon: {
                            TenKichThuoc: listProducts[key]?.ChiTietKichThuocSanPham?.KT0003?.TenKichThuoc,
                            Gia: listProducts[key]?.ChiTietKichThuocSanPham?.KT0003?.Gia
                        },
                    }
                }
                allProducts.push(productData);
            }
        }

        setProducts([...allProducts])
    }
    
    useEffect(() => {
        handleGetProducts();
    }, [])

    return (
        <View className="flex-1">
            <ScrollView>
                <View style={{ height: hp(35) }} className="bg-yellow-950 space-y-5">
                    {/* Header */}
                    <View style={{ width: wp(90) }} className="flex-row justify-between mx-auto mt-20 items-center">
                        <View>
                            <Text className="text-white">Giao đến</Text>
                            <TouchableOpacity onPress={() => navigation.navigate("MapView")}>
                                <Text className="text-white text-base font-semibold" style={{ width: wp(70) }}>
                                    {addressData?.DiaChi}
                                </Text>
                            </TouchableOpacity>
                        </View>
                        <View>
                            <Image
                                source={require("../assets/images/avtDemo.png")}
                                style={{ width: hp(8), height: hp(8) }}
                            />
                        </View>
                    </View>

                    {/* Search bar */}
                    <View className="mx-5 py-2 px-3 bg-white rounded-lg">
                        <View className="flex-row justify-between">
                            <TextInput placeholder="tìm món..." className="text-lg" />

                            <TouchableOpacity onPress={handleGetProducts} className="p-1 bg-yellow-950 rounded-lg">
                                <Icons.MagnifyingGlassIcon size={24} color="#ffffff" />
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>

                <View className="-mt-20">
                    {/* carousel */}
                    <Carousel
                        loop
                        width={wp(100)}
                        height={hp(22)}
                        autoPlay={true}
                        data={[
                            { id: 1, image: require("../assets/images/Frame 17.png") },
                            { id: 2, image: require("../assets/images/Frame 17.png") },
                            { id: 3, image: require("../assets/images/Frame 17.png") },
                            { id: 4, image: require("../assets/images/Frame 17.png") },
                        ]}
                        scrollAnimationDuration={1000}
                        renderItem={({ item }) => (
                            <View className="justify-center items-center">
                                <Image
                                    source={item.image}
                                    resizeMode="contain"
                                    style={{ width: wp(90), height: hp(20) }}
                                />
                            </View>
                        )}
                    />
                </View>

                {/* categories */}
                <View className="mx-2">
                    <Categories categories={categories} />
                </View>

                {/* card item */}
                <View className="mx-5 mt-5 flex-row flex-wrap justify-between">
                    {products && products.map((product, index)=> {
                        return (
                            <Item product={product} key={product.MaSanPham}/>
                        )
                    })}
                </View>
            </ScrollView>

            {/* cart */}
            <Draggable x={wp(80)} y={hp(82)} renderSize={24} renderColor="amber" isCircle>
                <View>
                    <TouchableOpacity
                        onPress={() => navigation.navigate("Cart")}
                        className="p-5 bg-yellow-600 rounded-full">
                        <Icons.ShoppingCartIcon size={30} strokeWidth={2} color={colors.primary} />
                        <View className="absolute -right-1 top-1 bg-red-500 px-2 rounded-full">
                            <Text className="text-white text-base">{quantity}</Text>
                        </View>
                    </TouchableOpacity>
                </View>
            </Draggable>
        </View>
    );
};

export default HomeScreen;
