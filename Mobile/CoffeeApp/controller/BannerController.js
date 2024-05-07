import { child, get, getDatabase, ref } from "firebase/database"

const getBanner = async () => {
    const dbRef = ref(getDatabase());
    try {
        const bannersSnapshot = await get(child(dbRef, "Banner"));
        const banners = bannersSnapshot.val();

        bannersList = Object.values(banners)

        return bannersList
    } catch (error) {
        console.log(error)
        return error        
    }
}

export { getBanner }