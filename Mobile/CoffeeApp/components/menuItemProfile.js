import { View, Text, TouchableOpacity } from 'react-native'
import * as Icons from 'react-native-heroicons/solid'
import React from 'react'
import { colors } from '../theme'

const MenuItemProfile = ({icon, title, handleClick}) => {
  return (
    <View className='p-4 border rounded-[14px] mt-10' style={{borderColor: '#9d9d9d'}}>
        <TouchableOpacity className='flex-row justify-between items-center' onPress={handleClick}>
            <View className='flex-row items-center gap-5'>
                {
                    icon === 'BookmarkIcon' ? (
                        <Icons.BookmarkIcon size={30} color={colors.active} />
                    ) : icon === 'HeartIcon' ? (
                        <Icons.HeartIcon size={30} color={colors.active} />
                    ) : icon === 'Key' ? (
                        <Icons.KeyIcon size={30} color={colors.active} />
                    ) : (
                        <Icons.ClipboardDocumentListIcon size={30} color={colors.active} />
                    )
                }
                <Text className='text-lg font-semibold'>{title}</Text>
            </View>

            <TouchableOpacity onPress={handleClick}>
                <Icons.ChevronRightIcon size={30} color={colors.text(0.5)} />
            </TouchableOpacity>
        </TouchableOpacity>
    </View>
  )
}

export default MenuItemProfile