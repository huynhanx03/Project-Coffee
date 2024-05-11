import { View, Text, TextInput, TouchableOpacity } from "react-native";
import React from "react";
import { colors } from "../theme";
import * as Icons from "react-native-heroicons/outline";
import { useNavigation } from "@react-navigation/native";

const InputCustom = ({ label, content, isEdit }) => {
    const navigation = useNavigation();
    const handleChangePage = () => {
        navigation.navigate("ChangeInfo", {label, content});
    }
    return (
        <>
            <Text
                className="font-semibold text-base ml-1"
                style={{ color: colors.text(1) }}
            >
                {label}
            </Text>
            <TouchableOpacity onPress={handleChangePage} disabled={isEdit ? false : true} className="space-y-1 mb-4 mt-1">
                <View
                    className="border rounded-lg"
                    style={{ borderColor: "#9d9d9d" }}
                >
                    <Text className="p-3 text-base">{content}</Text>
                    
                    {
                        isEdit && (

                            <Icons.ChevronRightIcon
                                size={24}
                                className="absolute right-3 top-3"
                                color={colors.active}
                            />
                        )
                    }
                </View>
            </TouchableOpacity>
        </>
    );
};

export default React.memo(InputCustom);
