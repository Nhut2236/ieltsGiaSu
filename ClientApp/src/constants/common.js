export const CLEANING_SERVICE = "cleaningService";
export const SECURITY_SERVICE = "sucurityService";
export const CUSTOMER_SERVICE = "customerService";
export const QUESTION_FEEDBACK = [
    { code: CLEANING_SERVICE, name: "Bạn thấy lớp học, văn phòng ALT IELTS Gia Sư có Sạch Sẽ không?" },
    { code: SECURITY_SERVICE, name: "Bạn có hài long với dịch vụ Bảo Vệ của ALT IELTS Gia SƯ không?"},
    { code: CUSTOMER_SERVICE, name: "Bạn có hài long với dịch vụ Chăm Sóc Khách Hàng của ALT IELTS Gia SƯ không?"}
];
// dropdown options
export const CHANGE_SERVICE = "changeService";
export const SIGN_OUT = "signOut";
export const DROPDOWN_OPTION = [
    { code: CHANGE_SERVICE, name: "Đổi dịch vụ", link: "/" },
    { code: SIGN_OUT, name: "Thoát", link: "/" },
];
export const EMOTION = [
    { rate: 0, text: "Rất không sạch sẽ", img: "crying"},
    { rate: 1, text: "Không sạch sẽ", img: "sad"},
    { rate: 2, text: "Chấp nhận được", img: "smile"},
    { rate: 3, text: "Sạch sẽ", img: "happy"},
    { rate: 4, text: "Rất sạch sẽ", img: "in-love"},
]