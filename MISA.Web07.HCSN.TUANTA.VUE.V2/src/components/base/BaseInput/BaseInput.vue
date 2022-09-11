.input {
    outline: none;
    height: 35px;
    border: 1px solid #afafaf;
    border-radius: 3px;
    padding: 0;
    margin: 0;
    cursor: pointer;
    text-indent: 14px;
    font-size: 14px;
    width: 100%;
}

.input:focus {
    border-color: #1aa4c8;
}


.input-search {
    background-color: #ffffff;
    border-radius: 2.5px;
    display: flex;
    align-items: center;
    position: relative;
    width: 179px;
}


.input__search--icon__item {
    background-position: -243px -23px;
    background-repeat: no-repeat;
}

.input::placeholder {
    text-overflow: ellipsis;
    width: 100%;
    color: #000;
}

.input-search input::placeholder{
    font-style: italic;
    color: #bbb;
}

.input__search--icon {
    width: 18px;
    height: 18px;
    margin: 9px 0 9px 10px;
    position: absolute;
}

.input__search--text {
    width: 100%;
    overflow: hidden;
    padding: 0;
    padding-left: 20px;
}

.number {
    width: 50px;
    border: none;
    color: black;
    font-weight: bold;
    outline: none;
    background-color: rgba(26, 164, 200, .2);
}

.input-checkbox {
    accent-color: #3b8fa2;
    padding: 0;
    margin: 0 20px 0 16px;
    display: flex;
    width: 16px;
    height: 16px;
    align-items: center;
    justify-content: center;
}

.input-width {
    width: calc(100% - 2px);
    padding-right: 14px;
    box-sizing: border-box;
}

.date {
    text-indent: 0 !important;
    text-align: center;
}