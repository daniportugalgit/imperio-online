'use strict'

class AcademyResearcher {

    constructor(academy) {
      this.academy = academy
      if (!this.academy) {
        this.academy = {}
        this.init()
      }
    }
  
    init() {
      this.create('aca_s_d_min', 1)
      this.create('aca_s_d_max', 6)
      this.create('aca_s_a_min', 1)
      this.create('aca_s_a_max', 6)
  
      this.create('aca_t_d_min', 1)
      this.create('aca_t_d_max', 12)
      this.create('aca_t_a_min', 1)
      this.create('aca_t_a_max', 12)
  
      this.create('aca_n_d_min', 1)
      this.create('aca_n_d_max', 12)
      this.create('aca_n_a_min', 1)
      this.create('aca_n_a_max', 12)
  
      this.create('aca_ldr_1_d_min', 1)
      this.create('aca_ldr_1_d_max', 12)
      this.create('aca_ldr_1_a_min', 1)
      this.create('aca_ldr_1_a_max', 12)
      this.create('aca_ldr_1_h1', 0)
      this.create('aca_ldr_1_h2', 0)
      this.create('aca_ldr_1_h3', 0)
      this.create('aca_ldr_1_h4', 0)
  
      this.create('aca_ldr_2_d_min', 1)
      this.create('aca_ldr_2_d_max', 12)
      this.create('aca_ldr_2_a_min', 1)
      this.create('aca_ldr_2_a_max', 12)
      this.create('aca_ldr_2_h1', 0)
      this.create('aca_ldr_2_h2', 0)
      this.create('aca_ldr_2_h3', 0)
      this.create('aca_ldr_2_h4', 0)
  
      this.create('aca_ldr_3_d_min', 1)
      this.create('aca_ldr_3_d_max', 12)
      this.create('aca_ldr_3_a_min', 1)
      this.create('aca_ldr_3_a_max', 12)
      this.create('aca_ldr_3_h1', 0)
      this.create('aca_ldr_3_h2', 0)
      this.create('aca_ldr_3_h3', 0)
      this.create('aca_ldr_3_h4', 0)
  
      this.create('aca_v_1', 0)
      this.create('aca_v_2', 0)
      this.create('aca_v_3', 0)
      this.create('aca_v_4', 0)
      this.create('aca_v_5', 0)
      this.create('aca_v_6', 0)

      this.create('aca_a_1', 0)
      this.create('aca_a_2', 0)
      this.create('aca_c_1', 0)
      this.create('aca_d_1', 0)
      this.create('aca_i_1', 0)
      this.create('aca_i_2', 0)
      this.create('aca_i_3', 0)

      this.create('aca_pea_id', null)
      this.create('aca_pea_min', 0)
      this.create('aca_pea_pet', 0)
      this.create('aca_pea_ura', 0)
      this.create('aca_pea_ldr', 0)

      this.create('aca_av_1', 0)
      this.create('aca_av_2', 0)
      this.create('aca_av_3', 0)
      this.create('aca_av_4', 0)

      this.create('aca_pln_1', 0)
      this.create('aca_art_1', 0)
      this.create('aca_art_2', 0)
    }
   
    create(researchId, value) {
      this.academy[researchId] = value
    }

    start(researchId, leaderId) {
      this.academy.aca_pea_id = researchId
      this.academy.aca_pea_ldr = leaderId
    }
  
    finish() {
      this.academy[this.academy.aca_pea_id] += 1
      this.clearOngoing()
    }

    invest(min, pet, ura) {
      this.academy.aca_pea_min += min
      this.academy.aca_pea_pet += pet
      this.academy.aca_pea_ura += ura
    }

    clearOngoing() {
      this.academy.aca_pea_id = null
      this.academy.aca_pea_min = 0
      this.academy.aca_pea_pet = 0
      this.academy.aca_pea_ura = 0
      this.academy.aca_pea_ldr = 0
    }
  }

  module.exports = AcademyResearcher
