<template>
<div class ="container">
    <Navbar/>
    <Container/>
</div>
      

</template>

<script>
import Navbar from "./components/layouts/LayoutNavbar/TheNavbar.vue"
import Container from "./components/layouts/LayoutContainer/TheContainer.vue"
export default {
    name: 'MISA.Web07.HCSN.TUANTA.VUE.v2',
    components:{
        Navbar,Container
    },
    data() {
        return {
            
        };
    },

    methods: {        
        
    },
};
</script>

<style scoped>

</style>