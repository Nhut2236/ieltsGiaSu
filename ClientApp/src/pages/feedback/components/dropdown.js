import React from "react";
import { Menu, Dropdown } from 'antd';
import { DownOutlined } from '@ant-design/icons';
import { DROPDOWN_OPTION, CHANGE_SERVICE, SIGN_OUT } from "../../../constants/common";
import "../styles.css";
const DropdownService = (props) => {
    const { setToggleService, setToggleLogout } = props;
    const openModal = (type) => {
        switch(type){
            case CHANGE_SERVICE:
                setToggleService(true);
                break;
            case SIGN_OUT:
                setToggleLogout(true);
                break;
            default:
                break;
        }
    };

    const exportExcel = async () =>{
        const apiPath = `/api/Report/CommentList`;
        await fetch(apiPath, {
            method: 'GET',
            headers: {
              'Accept': 'application/json',
              'Content-Type':'application/json',
              'Authorization': `Bearer ${localStorage.getItem('token')}`,
            },
          }).then(res => {
            res.json().then(data =>{
                if(data && data.data){
                    window.open(data.data);
                }
            })
          });
    };

    const menu = (
        <Menu>
          <Menu.Item>
              <div onClick={()=>exportExcel()}>
                Xuất ds comment
              </div>
          </Menu.Item>
          {DROPDOWN_OPTION.map( (option) =>             
            <Menu.Item key={option.code}>
                <div onClick={ () => openModal(option.code) }>
                    {option.name}
                </div>
            </Menu.Item>        
          )}       
        </Menu>
    );

    return (
        <Dropdown overlay={menu}>
            <a className="ant-dropdown-link" onClick={e => e.preventDefault()}>
                <div className="d-flex align-items-center">
                    <div className="dot"></div>
                    Tùy chọn <DownOutlined className="ml-2" />
                </div>
            </a>
        </Dropdown>
    )
};

export default DropdownService;