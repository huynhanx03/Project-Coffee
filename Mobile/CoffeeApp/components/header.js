import { View, Text, TouchableOpacity } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import * as Icons from "react-native-heroicons/outline";
import { useNavigation } from "@react-navigation/native";

const Header = ({title}) => {
    const navigation = useNavigation();
    return (
        <SafeAreaView
            style={{
                backgroundColor: "#f2f2f2",
                shadowColor: "#000000",
                shadowOffset: { width: 0, height: 4 },
                shadowOpacity: 0.19,
                shadowRadius: 5.62,
                elevation: 6,
            }}
        >
            {/* header */}

            <View className="flex-row justify-between items-center mx-5">
                <TouchableOpacity onPress={() => navigation.goBack()}>
                    <Icons.ChevronLeftIcon size={30} color={"black"} />
                </TouchableOpacity>
                <Text className="text-xl font-semibold">{title}</Text>

                <TouchableOpacity>
                    <Icons.HeartIcon size={30} color={"transparent"} />
                </TouchableOpacity>
            </View>
        </SafeAreaView>
    );
};

export default Header;
