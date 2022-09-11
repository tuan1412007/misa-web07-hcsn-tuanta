<template>
    <div class="combobox">
        <div v-on:click="btnClickShow()" class="combobox__control cbtooltip" :class="{ combobox__width: dialog_icon }" v-if="!ComboboxQuantity">
            <div v-show="icon" class="combobox__icon--left icon icon-filter icon__size-14 tooltip" >
                <Tooltip tooltiptext="Lọc dữ liệu" positiontooltip="bottom" style="margin-top: 10px" />
            </div>
            <input type="text" class="input combobox__input" v-bind:value="value_input" v-bind:placeholder="val" :class="{ 'border-red': borderRed }" />
            <div class="combobox__icon--right icon icon-down1 icon__size-5 tooltip" @mouseover="isShowTooltipOption = true" @mouseleave="isShowTooltipOption = false" >
                <Tooltip tooltiptext="hiển thị" positiontooltip="bottom" style="margin-top: 10px" v-if="isShowTooltipOption" />
                <div id="TooltipComBoBox" class="tooltip" v-show="isShowTooltip">
                    <Tooltip :tooltiptext="optionText" positiontooltip="bottom" id="tooltipText" />
                </div>
            </div>
        </div>

        <div v-show="isShow" class="combobox__option" v-if="optionTable == false" v-clickoutside="hideListData" >
            <div v-for="(item, index) in option" :key="item" class="combobox__option--item tooltip tooltipCombobox" @click="btnTickedBody(item)" ref="item" @mouseover="getPos(item, index)" @mouseleave="removePos()" :class="{ 'combobox__option--active': isTicked == item }" style="position: relative" >
                <span v-if="isTicked == item" class="combobox__option--item-icon" >
                    <i class="fa-solid fa-check"></i>
                </span>
                <div class="combobox__option--item-text">
                    {{ item[name] }}
                </div>
            </div>
        </div>

        <div class="combobox__control combox--number" v-if="ComboboxQuantity" v-on:click="isShowCombo = true" v-clickoutside="hideListData" >
            <input type="text" class="input combobox__input--number" v-bind:value="value_quantity" />
            <div class="combobox__icon--right icon icon-down1 icon__size-5 tooltip" title="chọn để chọn các thành phần" >
            </div>
        </div>

        <div v-show="isShowCombo" class="combobox__option combobox_quantity" v-if="optionTable == false" @mouseleave="isShowCombo = false" >
            <div v-for="item in combobox_quantity" :key="item" class="combobox__option--item" v-on:click="btnTickedQuantity(item)" :class="{ 'combobox__option--active': isTicked == item }" >
                <div class="combobox__option--item-text" style="padding: 0; margin-left: 40%" >
                    {{ item }}
                </div>
            </div>
        </div>

        <div v-show="isShow" class="combobox__option remove__Combobox" v-if="optionTable" v-clickoutside="hideListData" >
            <div class="combobox__option--table">
                <table style="width: 100%" class="table">
                    <thead class="combobox__table--header">
                        <tr>
                        <th class="text-align-center" style="width: 60px">Mã</th>
                        <th class="text-align-left" style="width: calc(100% - 60px)">
                            Tên
                        </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr :class="{ 'combobox__option--active': isTicked == item }" v-for="item in option" :key="item" v-on:click="btnTicked(item)" >
                        <td class="text-align-center idValue">{{ item[code] }}</td>
                        <td class="text-align-left NameValue">{{ item[name] }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>
<script>

/**
 * Nếu người dùng click chuột ra ngoài thì ẩn combobox
 * Author : TUANTA
 * Date : 10/08/2022
 */
const clickoutside = {
    mounted(element, binding) {
        try {
        element.clickOutsideEvent = (event) => {
            if (element.previousElementSibling != null) {
            if (
                !(
                element === event.target ||
                element.contains(event.target) ||
                element.previousElementSibling.contains(event.target)
                )
            ) 
            {
                binding.value(event, element);
            }
            }
        };
        } catch (error) {
        console.log(error);
        }
        document.body.addEventListener("click", element.clickOutsideEvent);
    },
    beforeUnmount: (element) => {
        console.log("beforeUnmount");
        document.body.removeEventListener("click", element.clickOutsideEvent);
    },
};

import Tooltip from "../../base/BaseTooltip/TheTooltip.vue";
export default {
    name: "ComboBox",
    components: {
        Tooltip,
    },
    directives: {
        clickoutside,
    },

    data() {
        return {
            isShowTooltipOption: false,
            isShow: false,
            isShowCombo: false,
            isShowTooltip: false,
            handlerCombobox: true,
            handlerOne: false,
            handlerTwo: false,
            name: "",
            id: "",
            code: "",
            isTicked: "", 
            value_input: "",
            left: "0px",
            top: "0px",
            optionText: "",
            value_quantity: "20",
            opacity: 0,
            combobox_quantity: [10, 20, 30, 40, 50],
            itemDetail: [],
            searchArray: {
                keyword: "",
                categoryAssetID: "",
                departmentID: "",
                dataPage: {
                    pageSize: "",
                },
            },
        };
    },
    props: {
        borderRed: {
            type: Boolean,
        },
        icon: {
            type: Boolean,
        },
        dialog_icon: {
            type: Boolean,
        },
        isShowInDialog: {
            type: Boolean,
        },
        isCheck: {
            type: Boolean,
        },
        optionTable: {
            type: Boolean,
            default: false,
        },
        ComboboxQuantity: {
            type: Boolean,
            default: false,
        },
        val: {
            type: String,
        },
        nameTable: {
            type: String,
        },
        option: {
            type: Array,
        },
        dataOption: {
            type: Array,
        },
        
        dataItemCombobox: {
            type: Array,
        },
    },

    methods: {
        /**
         * Hiển thị select-option Combobox
         * Author: TUANTA
         * Date:10/08/2022
         */
        btnClickShow() {
            this.isShow = true;
        },

        /**
         * Ẩn combobox khi click ra ngoài
         * Author : TUANTA
         * Date :10/08/2022
         */
        hideListData() {
            this.isShow = false;
        },

        /**
         * Chọn row nào thì focus vào row đó
         * Author : TUANTA
         * Date :10/08/2022
         */
        btnTicked(item) {
            try {
                this.isTicked = item;
                this.isShow = false;
                //Kiểm tra bảng mà combobox lấy dữ liệu
                this.value_input = item[this.code];
                this.$emit("checkRequired", true);
                if (this.isCheck == true) {
                    this.$emit("removeData");
                    this.$emit("checkFalse");
                }
                this.$emit("dataName", item);
            } 
            catch (error) {
                console.log(error);
            }
        },

        /**
         * show select-option combobox ở bên ngoài table
         * Author : TUANTA
         * Date : 10/08/2022
         */
        btnTickedBody(item) {
            if (this.isTicked == item) {
                this.isTicked[this.id] = "";
                this.value_input = "";
                this.isShow = false;
                this.$emit("remove", this.isTicked);
            } 
            else {
                this.isTicked = item;
                this.isShow = false;
                this.value_input = item[this.name];
                this.$emit("dataComBoBoxSearch", item);
            }
        },

        /**
         * Show combobox chọn số lượng record hiển thị
         * Author : TUANTA
         * Date : 10/08/2022
         */
        btnTickedQuantity(item) {
            this.isTicked = item;
            this.isShowCombo = false;
            this.value_quantity = item;
            this.dataPage = item;
            this.emitter.emit("dataPageSize", this.dataPage);
        },

        /**
         * Lấy vị trí của combobox
         * Author : TUANTA
         * Date :10/08/2022
         */
        getPos(item, index) {
            this.opacity = 1;
            const top = this.$refs.item[index].getBoundingClientRect().top;

            if (item.FixedAssetCategoryName != undefined) {
                if (item.FixedAssetCategoryName.length > 20) 
                {
                    this.isShowTooltip = true;
                    this.optionText = item.FixedAssetCategoryName;
                }
            } 
            else {
                if (item.DepartmentName.length > 20) 
                {
                    this.isShowTooltip = true;
                    this.optionText = item.DepartmentName;
                }
            }
            this.left = 30 + "px";
            this.top = top - 90 + "px";
        },

        /**
         * Đóng tooltip
         * Author : TUANTA
         * Date :10/08/2022
         */
        removePos() {
            this.isShowTooltip = false;
            this.optionText = "";
        },
    },

    /**
     * Bắn data cho combobox của dialog
     * Author : TUANTA
     * Date :15/08/2022
     */
    beforeUpdate() {
        if (this.dataItemCombobox != undefined) 
        {
            if ( this.optionTable && this.dataItemCombobox.departmentID != "" && this.dataItemCombobox.fixedAssetCategoryID != "" ) 
            {
                if (this.isCheck == false) 
                {
                    for (let i = 0; i < this.option.length; i++) 
                    {
                        var idChange = this.id.charAt(0).toLowerCase() + this.id.slice(1);
                        if (this.option[i][this.id] == this.dataItemCombobox[idChange]) 
                        {
                            this.$emit("isCheck");
                            this.btnTicked(this.option[i]);
                        }
                    }
                }
            }
        }
    },

    mounted() 
    {
        // lấy tên bảng cho combobox
        if (this.nameTable != "") 
        {
            this.name = this.nameTable + "Name";
            this.id = this.nameTable + "ID";
            this.code = this.nameTable + "Code";
        }
        this.$emit("checkRequired", false);
    },
};
</script>

<style scoped>
@import url(./combobox.css);
@import url(../BaseInput/input.css);

#TooltipComBoBox {
    left: v-bind("left");
    top: v-bind("top");
}

#tooltipText {
    opacity: v-bind("opacity");
    position: initial;
}
</style>
